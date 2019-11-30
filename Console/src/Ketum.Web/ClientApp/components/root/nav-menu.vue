<template>

  <!-- Layout sidenav -->
  <div
    id="layout-sidenav"
    class="sidenav sidenav-vertical layout-sidenav bg-sidenav-theme"
  >
    <ul class="sidenav-inner py-1">
      <template v-for="(route, index) in routes">
        <li v-if="route.divider" :key="index" class="sidenav-divider mt-0 mb-0"></li>
        <li v-else-if="!route.hidden"
          class="sidenav-item"
          :key="index"
        >
          <router-link
            :to="route.path"
            exact-active-class="active"
            class="sidenav-link"
            v-if="route.component"
          >
            <icon
              :icon="route.icon"
              class="mr-2"
            /><span>{{ route.display }}</span>
          </router-link>
          <a
            class="sidenav-link"
            :href="route.path"
            v-else
          >
            <icon
              :icon="route.icon"
              class="mr-2"
            /><span>{{ route.display }}</span>
          </a>
        </li>
      </template>
    </ul>
  </div>
  <!-- / Layout sidenav -->
</template>

<script>
import { routes } from "@/router/routes";
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