<template>
  <div>
    <page-head icon="chart-line" title="All Monitorings" />
    <div class="row">
      <div class="col-md-3" v-for="(monitoring, index) in monitorings" :key="index">
        <div class="card  mr-4 mb-4">
          <img class="card-img-top shadow-sm"
            src="https://api.apiflash.com/v1/urltoimage?access_key=ea5fe25b67544bc2b9424e8748ac6eb8&url=http://berkansasmaz.com"
            alt="">
          <div class="card-body">
            <h4 class="card-title">Card title</h4>
          </div>
          <div class="card-body">
            <router-link 
					:to="{name: 'monitoring-view' , params: {id:monitoring.monitorId}}"
					class="card-link"
			>
              View Dashboard
            </router-link>
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
        this.monitorings.push(...result.data);
    }
  };

</script>
