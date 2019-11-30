<template>
  <div>
    <page-head
      icon="plus"
      title="New Monitoring"
    />
    <button @click="save">
      Save
    </button>
  </div>
</template>

<script>
import service from "service/monitoring";
export default {
  methods: {
    async save() {
      var result = service.save({
        name: "berkan",
        url: "https://berkansasmaz.com/"
      });
    }
  }
};
</script> 

