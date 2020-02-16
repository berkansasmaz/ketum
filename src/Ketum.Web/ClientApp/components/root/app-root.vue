<template>
  <!-- / Layout navbar -->

  <div
    class="layout-container"
    id="app"
  >

    <nav-menu params="route: route"></nav-menu>

    <div class="layout-content">
      <!-- Page content -->
      <div class="container-fluid flex-grow-1 container-p-y">
		  	<notifications/>
        	<router-view></router-view>
      </div>
      <!-- / Page content -->
    </div>
  </div>
</template>

<script>
import NavMenu from "./nav-menu";
export default {
  components: {
    "nav-menu": NavMenu
  },
  data() {
    return {};
  }
};
</script>

<style>
</style>