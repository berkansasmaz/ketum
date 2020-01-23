<template>
  <div>
    <page-head icon="chart-line" title="All Monitorings" />
    <div class="row">
      <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12" v-for="(monitoring, index) in monitorings" :key="index">
        <div class="card  mr-2 mb-2">
          <apexchart  type="area" :options="monitoring.chart" :series="monitoring.chart.series" class="mt-4 ml-2 mr-2"></apexchart>
          <div class="card-body text-center">
            <h4 class="card-title">{{monitoring.name}} 
			</h4>
			<div class="btn-group">
				<router-link :to="{name: 'monitoring-view' , params: { id:monitoring.monitorId }}" class="btn btn-sm btn-secondary">
					Dashboard
				</router-link>
				<router-link :to="{name: 'monitoring-save' , params: { id:monitoring.monitorId }}" class="btn btn-sm btn-primary">
					Edit
				</router-link>
			</div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
  import service from "service/monitoring";
  export default {
    data() {
      return {
        monitorings: []
      }
    },
    async mounted() {
      var result = await service.list();
      if (result.data && result.data.length)
		result.data.map(item => {
			item.chart = {
				chart: {
					type: 'area',
					height: 160,
					sparkline: {
					enabled: true
					},
				},
				stroke: {
					curve: 'straight'
				},
				fill: {
					opacity: 0.3,
				},
				series: [{
					data: this.randomize()
				}],
				yaxis: {
					min: 0
				},
				colors: ['#DCE6EC'],
				title: {
					text: '99,34 %',
					offsetX: 0,
					style: {
					fontSize: '20pt',
					cssClass: 'apexcharts-yaxis-title'
					}
				},
				subtitle: {
					text: 'Uptime',
					offsetX: 0,
					style: {
					fontSize: '14px',
					cssClass: 'apexcharts-yaxis-title'
					}
				}
        };
			this.monitorings.push(item);
		});
},
    methods: {
      randomize() {
        let arg = [47, 45, 54, 38, 56, 24, 65, 31, 37, 39, 62, 51, 35, 41, 35, 27, 93, 53, 61, 27, 54, 43, 19, 46];
        var array = arg.slice();
        var currentIndex = array.length,
          temporaryValue, randomIndex;

        while (0 !== currentIndex) {

          randomIndex = Math.floor(Math.random() * currentIndex);
          currentIndex -= 1;

          temporaryValue = array[currentIndex];
          array[currentIndex] = array[randomIndex];
          array[randomIndex] = temporaryValue;
        }
        return array;
      }
    },
  };

</script>
