<template>

  <!-- Layout sidenav -->
  <div
    id="layout-sidenav"
    class="sidenav sidenav-vertical layout-sidenav bg-sidenav-theme"
  >
    <ul class="sidenav-inner py-1">
      <li
        class="sidenav-item"
        v-for="(route, index) in routes"
        :key="index"
      >
        <router-link
          :to="route.path"
          exact-active-class="active"
          class="sidenav-link"
        >
          <icon
            :icon="route.icon"
            class="mr-2"
          /><span>{{ route.display }}</span>
        </router-link>
      </li>
    </ul>
  </div>
  <!-- / Layout sidenav -->
</template>

<script>
import { routes } from "../router/routes";
export default {
  data() {
    return {
      routes,
      collapsed: true
    };
  },
  methods: {}
};
</script>

<style scoped>
</style>