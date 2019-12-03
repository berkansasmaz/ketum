<template>
  <div>
    <page-head
      icon="plus"
      title="New Monitoring"
    />
	<input type="text" v-model="model.name"/>
    <button @click="save">
      Save
    </button>
  </div>
</template>

<script>
import service from "service/monitoring";
export default {
	data() {
		return {
			model: { 
				name: ""
			}
		}
	},
  methods: {
    async save() {
      var result = service.save({
        name: this.model.name
      });
    }
  }
};
</script> 

