<template>
  <div>
    <div v-if="!item">
      <content-placeholders>
        <content-placeholders-text :lines="3"/>
      </content-placeholders>
    </div>
    <div v-if="item">
      <page-head title="Alerts Channels" :prefix="item.name"/>
      <div class="card">
        <div class="card-header">
          <button class="btn btn-sm btn-secondary" v-b-modal.modallg>
            <icon icon="bell"/>&nbsp;Add a new alert channel
          </button>
        </div>
        <div class="card-body">
          <div
            v-if="!alerts"
            class="alert alert-info"
          >You don't have any alert channel yet. If you want to add it, please click "Add a new alert channel" button.</div>
          <div class="list-group" v-if="alerts">
            <div class="list-group-item" v-for="alert in alerts" :key="alert.id">
              <div class="d-flex w-100 justify-content-between">
                <h5 class="mb-1">{{alert.title}}</h5>
              </div>
              <p class="mb-1">{{alert.channelTypeText}}</p>
            </div>
          </div>
        </div>
      </div>
    </div>
    <b-modal hide-footer id="modallg" size="lg" title="Add a new alert">
      <div class="form-group">
        <label>Select your alert type</label>
        <b-form-select v-model="model.channelType" :options="alertOptions" class="mb-3">
          <template slot="first">
            <option :value="0" disabled>-- Select your alert type --</option>
          </template>
        </b-form-select>
      </div>
      <div v-if="model.channelType!='0'">
        <b-form-group label="Title" label-for="title">
          <b-form-input id="title" v-model="model.title" trim/>
        </b-form-group>
        <alert-email
          :value="model.settings"
          v-model="model.settings"
          v-if="model.channelType=='1'"
        />
        <alert-sms v-if="model.channelType=='2'"/>
        <alert-webhook v-if="model.channelType=='3'"/>
        <alert-slack v-if="model.channelType=='4'"/>
        <alert-telegram v-if="model.channelType=='5'"/>
        <button @click="addAlertChannel" class="btn btn-success">
          <icon icon="check"/>&nbsp;Save Alert Channel
        </button>
      </div>
    </b-modal>
  </div>
</template>

<script>
  import alertEmail from "components/pages/monitoring/partial/alert-email";
  import alertSms from "components/pages/monitoring/partial/alert-sms";
  import alertSlack from "components/pages/monitoring/partial/alert-slack";
  import alertWebhook from "components/pages/monitoring/partial/alert-webhook";
  import alertTelegram from "components/pages/monitoring/partial/alert-telegram";
  import serviceMonitoring from "service/monitoring";
  import serviceAlert from "service/monitoralert";
  export default {
    components: {
      alertEmail,
      alertSms,
      alertSlack,
      alertWebhook,
      alertTelegram
    },
    data() {
      return {
        id: null,
        item: null,
        alerts: null,
        model: {
          title: "",
          settings: {},
          channelType: "0"
        },
        alertOptions: [
          {
            value: "1",
            text: "Email"
          },
          {
            value: "2",
            text: "SMS"
          },
          {
            value: "3",
            text: "Webhook"
          },
          {
            value: "4",
            text: "Slack"
          },
          {
            value: "5",
            text: "Telegram"
          }
        ]
      };
    },
    async mounted() {
      await this.monitor();
      await this.list();
    },
    methods: {
      async addAlertChannel() {
        console.log(`I'm adding alert ${JSON.stringify(this.model)}.`);
        const result = await serviceAlert.save(this.model);
        if (result.success) {
          await this.list();
          this.$root.$emit("bv::hide::modal", "modallg");
          this.model = {
            title: "",
            settings: {},
            channelType: "0"
          };
        }
      },
      async monitor() {
        const result = await serviceMonitoring.get(this.$route.params.id);
        if (result.success) {
          this.item = result.data;
          this.model.monitorId = this.item.monitorId;
        }
      },
      async list() {
        const result = await serviceAlert.list(this.$route.params.id);
        if (result.success) {
          this.alerts = result.data;
        }
      }
    }
  };
</script>
