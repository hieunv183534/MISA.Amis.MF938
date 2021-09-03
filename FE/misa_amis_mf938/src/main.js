import Vue from 'vue'
import App from './App.vue'
import axios from 'axios'
import VueAxios from 'vue-axios'

Vue.use(VueAxios, axios)

export const PopupEventBus = new Vue();
export const LoaderEventBus = new Vue();
export const ToastMessengerEventBus = new Vue();
export const EventBus = new Vue();

Vue.config.productionTip = false

new Vue({
  render: h => h(App),
}).$mount('#app')
