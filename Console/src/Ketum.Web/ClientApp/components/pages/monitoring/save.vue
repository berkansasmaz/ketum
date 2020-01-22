<template>
  <div >
	  	<div v-if="loading">
			<content-placeholders>
			<content-placeholders-heading :img="true" />
			<content-placeholders-text :lines="15" />
			</content-placeholders>
		</div>
		<div v-if="!loading">
			<page-head  :title="title" />
			<mvi-text title="Project Name" placeholder = "Type your project name" v-model="model.name"/>
			<mvi-text title="Project Url" placeholder = "Type your website url for test" v-model="model.url"/>
			
			<button @click="save" class="btn btn-success">
			<icon icon="plus"/>	Save
			</button>
		</div>
  </div>
</template>

<script>
  import service from "service/monitoring";
  import router from "@/router";
  export default {
    data() {
      return {
		loading: true,
        model: {
		  name: "",
		  url : ""
        }
      }
	},
	computed: {
		title(){
			return this.$route.params.id ? this.model.name : "New Monitoring";
		}
	},
	async mounted() {
		if (this.$route.params.id) {
			const result = await service.get(this.$route.params.id);
			this.loading = false;
			if (result.success) {
				console.log(result);
				this.model.name = result.data.name;
				this.model.url = result.data.url;
			}
			else{
				this.loading = false;
			}
		}
	},
    methods: {
		async save() {
			if (this.$route.params.id) {
				this.model.id  = this.$route.params.id;
			}
			let result = await service.save(this.model);
			if(result.data && result.data && result.data.id){
					router.push({
						name: "monitoring-list"
					})
			}
		}
	}
  };

</script>
