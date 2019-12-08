<template>
  <div>
    <page-head icon="plus" title="New Monitoring" /> 
   <mvi-text title="Project Name" placeholder = "Type your project name" v-model="model.name"/>
    <mvi-text title="Project Url" placeholder = "Type your website url for test" v-model="model.url"/>
	
    <button @click="save" class="btn btn-success">
		<icon icon="plus"/>	Save
    </button>

  </div>
</template>

<script>
  import service from "service/monitoring";
  import router from "@/router";
  export default {
    data() {
      return {
        model: {
		  name: "",
		  url : ""
        }
      }
    },
    methods: {
		async save() {
			var result = await service.save(this.model);
			if(result.data && result.data && result.data.id){
					router.push({
						name: "monitoring-list"
					})
			}
		}
	}
  };

</script>
