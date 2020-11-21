<template>
  <div class="my-3">
    <form>
      <div class="form-group">
        <label for="filter">Search</label>
        <div>
          <input id="filter" v-model="filter" type="text" class="form-control">
        </div>
      </div>
      <div>
        <button class="btn btn-primary mr-2" @click.prevent="loadData()">
          Load All
        </button>
        <button class="btn btn-info mr-2" @click.prevent="loadData('byCurrency')">
          By Currency
        </button>
        <button class="btn btn-info mr-2" @click.prevent="loadData('byDateRange')">
          By Date Range
        </button>
        <button class="btn btn-info" @click.prevent="loadData('byStatus')">
          By Status
        </button>
      </div>
    </form>
    <table class="table my-3">
      <thead>
        <th v-for="col in columns" :key="col">
          {{ col }}
        </th>
      </thead>
      <tbody>
        <tr v-for="(record, index) in records" :key="`${record.id}-${record.code}`">
          <td v-for="field in fields" :key="field.key">
            {{ displayValue(record, index, field.key) }}
          </td>
        </tr>
      </tbody>
      <tfoot>
        <td :colspan="fields.length">
          <strong>Total Records</strong>
          <span class="badge badge-info ml-2">{{ totalRecords }}</span>
        </td>
      </tfoot>
    </table>
  </div>
</template>

<script>
export default {
  data() {
    return {
      filter: '',
      records: [],
      totalRecords: 0,
      fields: [
        {
          key: 'index',
          label: 'No.'
        },
        {
          key: 'code',
          label: 'Id'
        },
        {
          key: 'payment',
          label: 'Payment'
        },
        {
          key: 'transactionDate',
          label: 'Transaction Date'
        },
        {
          key: 'status',
          label: 'Status'
        }
      ]
    }
  },
  computed: {
    columns() {
      return this.fields.map(f => f.label)
    }
  },
  created() {
    this.loadData()
  },
  methods: {
    async loadData(filterType) {
      const restEndPoint = 'http://localhost:5000/api' // will get from config in actual project
      const api = `${restEndPoint}/transaction`
      const dates = this.filter.split(',')
      const mapping = {
        byCurrency: `${api}/currency/${this.filter}`,
        byDateRange: `${api}/transaction-date/${dates[0]}/${dates[1]}`,
        byStatus: `${api}/status/${this.filter}`
      }
      const url = filterType ? mapping[filterType.toString()] : api
      const { data } = await this.$http.get(url)
      
      this.records = data
      this.totalRecords = data.length
    },
    displayValue(record, index, key) {
      switch (key) {
        case 'index':
          return `${index + 1}.`
        case 'payment':
          return `${record.amount} ${record.currencyCode}`
        default:
          return record[key.toString()]
      }
    }
  }
}
</script>
