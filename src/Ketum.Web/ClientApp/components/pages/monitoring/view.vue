<template>
  <div>
    <template v-if="!item">
      <div>
        <content-placeholders>
          <content-placeholders-heading :img="true"/>
          <content-placeholders-text :lines="15"/>
        </content-placeholders>
      </div>
    </template>
    <template v-if="item">
      <page-head :prefix="this.item.name" title="Dashboard"/>
      <div class="row">
        <div class="col-sm-6 col-xl-6">

          <div class="card mb-4">
              <apexchart
                type="area"
                :options="item.uptimeChart"
                :series="item.uptimeChart.series"
                class="m-2 mt-4"
                height="140"
              />
          </div>

        </div>
        <div class="col-sm-6 col-xl-6">

          <div class="card mb-4">
              <apexchart
                type="area"
                :options="item.loadtimeChart"
                :series="item.loadtimeChart.series"
                class="m-2 mt-4"
                height="140"
              />
          </div>
        </div>
      </div>
      <div class="row">
        <div class="col-md-12">
          <div class="card mb-4">
            <h6 class="card-header with-elements">
              <div class="card-header-title">Stats</div>
              <div class="card-header-elements ml-auto d-none">
                <button type="button" class="btn btn-default btn-xs md-btn-flat">Show more</button>
              </div>
            </h6>
            <div class="table-responsive" v-if="steps">
              <table class="table card-table">
                <thead>
                <tr>
                  <th class="text-center">#</th>
                  <th>Type</th>
                  <th>Last Check Date</th>
                  <th>Status</th>
                  <th>Interval</th>
                </tr>
                </thead>
                <tbody>
                <tr v-for="(step, index) in steps" :key="'monitorstep' + index">
                  <td class="text-center">
                    <button v-b-modal.modal-xl @click="details(step.monitorStepId)"
                            class="btn btn-sm btn-outline-primary">
                      <icon icon="search"></icon>
                    </button>
                  </td>
                  <td>{{step.typeText}}</td>
                  <td>
                    <span :title="step.lastCheckDate">{{step.lastCheckDate | moment("from", "now")}} </span>
                  </td>
                  <td>
                    <ktv-monitor-status :status="step.status" :title="step.statusText"/>
                  </td>
                  <td>{{step.interval}} (seconds)</td>
                </tr>
                </tbody>
              </table>
            </div>
          </div>
        </div>
      </div>
    </template>
    <b-modal id="modal-xl" size="xl" title="Logs">
      <div v-if="!logs">
        <content-placeholders>
          <content-placeholders-heading :img="true"/>
          <content-placeholders-text :lines="15"/>
        </content-placeholders>
      </div>
      <div class="table-responsive" v-if="logs">
        <table class="table card-table">
          <thead>
          <tr>
            <th>Start</th>
            <th>End</th>
            <th>Status</th>
            <th>Interval</th>
            <th>Logs</th>

          </tr>
          </thead>
          <tbody>
          <tr v-for="(log, index) in logs.items" :key="'monitorsteplog' + index">
            <td>
              <span :title="log.startDate">{{log.startDate | moment("dddd, MMMM YYYY, h:mm:ss a")}} </span>
            </td>
            <td>
              <span :title="log.endDate">{{log.endDate | moment(`dddd, MMMM YYYY, h:mm:ss a`)}} </span>
            </td>
            <td>
              <ktv-monitor-status :status="log.status" :title="log.statusText"/>
            </td>
            <td>{{log.interval}} (seconds)</td>
            <td>{{log.log}}</td>
          </tr>
          </tbody>
        </table>
      </div>
      <b-pagination v-if="logs" class="mt-4" align="center" pills v-model="logsCurrentPage" :total-rows="logs.itemCount"
                    :per-page="10"/>
    </b-modal>
  </div>
</template>

<script>
  import service from "service/monitoring";

  export default {
    data() {
      return {
        id: null,
        item: null,
        steps: null,
        logs: null,
        logsCurrentPage: 1,
        currentStepId: null
      }
    },
    watch: {
      logsCurrentPage(newValue) {
        this.logPageChanged(newValue);
      }
    },
    async mounted() {
      var result = await service.get(this.$route.params.id);
      if (result.success) {
        let item = result.data;
        item.loadtimeChart = this.chart(
          `${item.loadTime.toFixed(2)} ms`,
          "Load time",
          item.loadTimes
        );
        item.uptimeChart = this.chart(
          `${item.upTime.toFixed(2)} %`,
          "Uptime",
          item.upTimes
        );
        this.item = item;
      }

      var steps = await service.steps(this.$route.params.id);
      if (steps.success) {
        this.steps = steps.data;
      }
    },
    methods: {
      async details(id) {
        this.currentStepId = id;
        await this.logPageChanged(1);
      },
      async logPageChanged(page) {
        this.logs = null;
        const result = await service.steplogs(this.currentStepId, page - 1);
        this.logs = result.data;
      },
      chart(title, subtitle, data) {
        return {
          chart: {
            type: "area",
            height: 160,
            sparkline: {
              enabled: true
            }
          },
          stroke: {
            curve: "smooth"
          },
          fill: {
            opacity: 0.3,
            type: 'gradient' / 'solid' / 'pattern' / 'image'
          },
          markers: {
            size: 0,
          },
          series: [
            {
              name: subtitle,
              data: data
            }
          ],
          yaxis: {
            min: 0
          },

          colors: ["#2E93fA"],
          title: {
            text: title,
            offsetX: 0,
            style: {
              fontSize: "16pt",
              cssClass: "apexcharts-yaxis-title"
            }
          },
          subtitle: {
            text: subtitle,
            offsetX: 0,
            style: {
              fontSize: "10pt",
              cssClass: "apexcharts-yaxis-title"
            }
          }
        };
      }
    }
  };
</script>
