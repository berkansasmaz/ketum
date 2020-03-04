import Vue from 'vue';
import axios from 'axios';
import router from './router/index';
import store from './store';
import { sync } from 'vuex-router-sync';
import App from 'components/root/app-root';
import { FontAwesomeIcon } from './icons';
import PageHead from 'components/shared/page-head';
import Notifications from 'vue-notification';
import VueContentPlaceholders from 'vue-content-placeholders';
import KTIText from 'components/Input/text';
import VueApexCharts from 'vue-apexcharts';
import KTVMonitorStatus from 'components/shared/monitor-status';
import { BootstrapVue} from 'bootstrap-vue';
import VueStripeCheckout from 'vue-stripe-checkout';

Vue.use(VueStripeCheckout, 'pk_test_WosCGsOkA5rIQqvvpDILwb2q');
// Install BootstrapVue
Vue.use(BootstrapVue);
Vue.use(VueContentPlaceholders);

Vue.use(VueApexCharts);
Vue.use(require('vue-moment'));

Vue.component('apexchart', VueApexCharts);

// Input Controls

Vue.use(Notifications);


// Registration of global components
Vue.component('icon', FontAwesomeIcon);
Vue.component('page-head', PageHead);
Vue.component('ktv-monitor-status', KTVMonitorStatus);

//Registration for Inputs Controls
Vue.component('mvi-text', KTIText);

// app.js
import 'bootstrap/dist/css/bootstrap.css'
import 'bootstrap-vue/dist/bootstrap-vue.css'

Vue.prototype.$http = axios;

sync(store, router);

const app = new Vue({
	store,
	router,
	...App
});

export {
	app,
	router,
	store
};
