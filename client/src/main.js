import Vue from 'vue'
import App from './App.vue'
import axios from "axios"
import VueAxios from "vue-axios"
import VueUploadComponent from "vue-upload-component"
import './plugin/filesize-format'

Vue.config.productionTip = false
Vue.use(VueAxios, axios)
Vue.component("file-upload", VueUploadComponent)

new Vue({
  render: h => h(App),
}).$mount('#app')
