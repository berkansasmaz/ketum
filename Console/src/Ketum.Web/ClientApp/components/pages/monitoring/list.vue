<template>
  <div>
    <page-head icon="chart-line" title="All Monitorings" />
    <div class="row">
      <div class="col-md-12" >
        <!-- Popular products -->
       <div class="card mb-3 col-md-3" v-for="(monitoring, index) in monitorings" :key="index">
                  <img class="card-img-top" src="https://via.placeholder.com/512" alt="Card image cap">
                  <div class="card-body">
                    <h4 class="card-title">{{monitoring.name}}</h4>
                    <p class="card-text">This is a wider card with supporting text below as a natural lead-in to additional content. This content is a little bit longer.</p>
                    <p class="card-text">
                      <small class="text-muted">Last updated 3 mins ago</small>
                    </p>
                  </div>
                </div>
        <!-- / Popular products -->
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
	  if(result.data && result.data.length)
	 	 this.monitorings.push(...result.data);
    }
  };

</script>
