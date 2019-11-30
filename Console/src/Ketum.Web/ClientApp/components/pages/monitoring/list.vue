<template>
  <page-head
    icon="chart-line"
    title="All Monitorings"
  />
</template>

<script>
import service from "service/monitoring";
export default {
  async mounted() {
    var result = await service.list();
  }
};
</script> 