<template>
  <div>
    <div v-if="loading">
      <content-placeholders>
        <content-placeholders-text :lines="3"/>
      </content-placeholders>
    </div>
    <div v-if="!loading">
      <page-head :title="title"/>
      <div
        class="alert alert-warning"
        v-if="noquota"
      ><icon icon="exclamation-triangle" /> You don't have enough quota to add new monitor. If you want to buy it more, please change your subscription.</div>
      <div v-if="!noquota">
        <mvi-text title="Project Name" placeholder="Type your project name" v-model="model.name"/>
        <mvi-text
          title="Project Url"
          placeholder="Type your website url for test"
          v-model="model.url"
        />
        <button @click="save" class="btn btn-success">
          <icon icon="plus"/> Save
        </button>
      </div>
    </div>
  </div>
</template>

<script>
  import service from "service/monitoring";
  import serviceSubscription from "service/subscription";
  import router from "@/router";
  export default {
    data() {
      return {
        loading: true,
        model: {
          name: "",
          url: ""
        },
        subscription: null,
        noquota: false,
        feature: null
      };
    },
    computed: {
      title() {
        return this.$route.params.id ? this.model.name : "New Monitoring";
      }
    },
    async mounted() {
      console.log(this.$route.params.id);
      if (this.$route.params.id) {
        let result = await service.get(this.$route.params.id);
        this.loading = false;
        if (result.success) {
          console.log(result);
          this.model.name = result.data.name;
          this.model.url = result.data.url;
        }
      } else {
        this.loading = false;
        this.subscription = (await serviceSubscription.current()).data;
        this.feature = this.subscription.features.find(
          x => x.name === "MONITOR"
        );
        if (this.feature) {
          const valueRemained = parseInt(this.feature.valueRemained);
          this.noquota = !valueRemained || valueRemained <= 0;
        }
      }
    },
    methods: {
      async save() {
        if (this.$route.params.id) {
          this.model.id = this.$route.params.id;
        }
        let result = await service.save(this.model);
        if (result.success && result.data && result.data.id) {
          router.push({
            name: "monitoring-list"
          });
        }
      }
    }
  };
</script>
