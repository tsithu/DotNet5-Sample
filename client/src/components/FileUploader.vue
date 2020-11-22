<template>
  <div class="example-simple">
    <h1 id="example-title" class="example-title">
      Simple Example
    </h1>
    <div class="upload">
      <ul>
        <li v-for="file in files" :key="file.id">
          <span>{{ file.name }}</span> -
          <span>{{ file.size | formatSize }}</span> -
          <span v-if="file.error">{{ file.error }}</span>
          <span v-else-if="file.success">success</span>
          <span v-else-if="file.active">active</span>
          <span v-else></span>
        </li>
      </ul>
      <div class="example-btn">
        <file-upload ref="upload"
                     v-model="files"
                     class="btn btn-primary"
                     post-action="http://localhost:5000/api/upload"
                     :extensions="extensions"
                     :accept="accept"
                     :multiple="false"
                     :size="size"
                     @input-filter="inputFilter"                   
                     @input-file="inputFile">
          <i class="fa fa-plus"></i>
          Select File
        </file-upload>
        <button v-if="!$refs.upload || !$refs.upload.active" type="button" class="btn btn-success" @click.prevent="$refs.upload.active = true">
          <i class="fa fa-arrow-up" aria-hidden="true"></i>
          Start Upload
        </button>
        <button v-else type="button" class="btn btn-danger" @click.prevent="$refs.upload.active = false">
          <i class="fa fa-stop" aria-hidden="true"></i>
          Stop Upload
        </button>
      </div>
    </div>
  </div>
</template>

<script>
export default {
    data() {
        return {
            files: [],
            extensions: 'csv,xml',
            accept: '.csv,.xml',
            size: 1024 * 1024
        }
    },
    created() {
      console.log('Created')
    },
    methods: {
        inputFilter(newFile, oldFile, prevent) {
            if (newFile && !oldFile) {
                // Before adding a file
                // Filter system files or hide files
                if (/(\/|^)(Thumbs\.db|desktop\.ini|\..+)$/.test(newFile.name)) {
                    return prevent()
                }
                // Filter php html js file
                if (/\.(php5?|html?|jsx?)$/i.test(newFile.name)) {
                    return prevent()
                }
            }
            return null
        },
        inputFile(newFile, oldFile) {
            if (newFile && !oldFile) {
                // add
                console.log('add', newFile)
            }
            if (newFile && oldFile) {
                // update
                console.log('update', newFile)
            }
            if (!newFile && oldFile) {
                // remove
                console.log('remove', oldFile)
            }
        }
    }
}
</script>

<style>
.file-uploads {
  margin-right: 7px;
}
</style>
