<template>
  <div class="container text-left">
    <file-uploader class="my-3"/>
    <hr>
    <data-viewer class="my-3"/>
  </div>
</template>

<script>
import DataViewer from './components/DataViewer.vue'
import FileUploader from './components/FileUploader.vue'

export default {
  name: 'App',
  components: {
    FileUploader,
    DataViewer
  }
}
</script>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  margin-top: 60px;
}
</style>
