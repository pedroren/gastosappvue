<template>
  <v-container>
    <v-layout row wrap>
      <v-flex xs12>
        <v-card>
          <v-card-text>
            <v-layout row wrap>
              <trans-filter
                :filtroCuenta.sync="filtroCuenta"
                :filtroConcepto.sync="filtroConcepto"
                :filtroFechaDesde.sync="filtroFechaDesde"
                :filtroFechaHasta.sync="filtroFechaHasta"
              ></trans-filter>
              <v-btn @click="refresh">Refresh</v-btn>
              <v-btn
                color="pink"
                dark
                class="hidden-md-and-up"
                @click="newItem"
              >
                <v-icon>add</v-icon>
              </v-btn>
            </v-layout>
            <v-btn
              color="pink"
              dark
              absolute
              bottom
              right
              fab
              class="hidden-sm-and-down"
              @click="newItem"
            >
              <v-icon>add</v-icon>
            </v-btn>
          </v-card-text>
        </v-card>
      </v-flex>
    </v-layout>
    <v-layout row>
      <v-flex xs12 md6>
        <v-text-field
          append-icon="search"
          label="Search"
          single-line
          hide-details
          v-model="search"
        ></v-text-field>
      </v-flex>
    </v-layout>
    <v-layout row>
      <v-flex xs12>
        <trans-create-edit
          :showDialog.sync="dialog"
          :transaccionId.sync="transaccionId"
          v-on:onsave="refresh"
          :contextDate="filtroFechaHasta"
          :contextCuenta="filtroCuenta"
          :contextConcepto="filtroConcepto"
        ></trans-create-edit>
        <v-data-table
          :headers="headers"
          :items="lista"
          item-key="TransaccionId"
          :search="search"
          :loading="loading"
          show-expand
          :sort-by="['Fecha']"
          :sort-desc="true"
          :items-per-page="20"
        >
          <!-- expand column  
      <template v-slot:item.data-table-expand="{ item, isExpanded, expand }">
        <v-btn @click="expand(true)" v-if="!isExpanded">Expand</v-btn>
        <v-btn @click="expand(false)" v-if="isExpanded">close</v-btn>
      </template>-->
          <template v-slot:item.ConceptoId="{ item }">
            <concepto-nombre :ConceptoId="item.ConceptoId"></concepto-nombre>
          </template>
          <template v-slot:item.CuentaId="{ item }">
            <cuenta-nombre :CuentaId="item.CuentaId"></cuenta-nombre>
          </template>
          <template v-slot:item.CuentaIdTransf="{ item }">
            <cuenta-nombre :CuentaId="item.CuentaIdTransf"></cuenta-nombre>
          </template>
          <template v-slot:item.Fecha="{ item }">
            {{ item.Fecha | date }}
          </template>
          <template v-slot:item.Monto="{ item }">
            {{ item.Monto | currency }}
          </template>
          <template v-slot:item.action="{ item }">
            <v-btn icon class="mx-0" @click="editItem(item)">
              <v-icon color="teal">edit</v-icon>
            </v-btn>
            <v-btn icon class="mx-0" @click="deleteItem(item)">
              <v-icon color="pink">delete</v-icon>
            </v-btn>
          </template>
          <!-- <template slot="item" slot-scope="props">
        <tr>
          <td>{{ props.item.Descripcion }}</td>
          <td><concepto-nombre :ConceptoId="props.item.ConceptoId"></concepto-nombre></td>
          <td><cuenta-nombre :CuentaId="props.item.CuentaId"></cuenta-nombre></td>
          <td><cuenta-nombre :CuentaId="props.item.CuentaIdTransf"></cuenta-nombre></td>
          <td>{{ props.item.Fecha | date }}</td>
          <td class="text-xs-right">{{ props.item.Monto | currency }}</td>
          <td class="justify-center layout px-0">
            <v-btn icon class="mx-0" @click="editItem(props.item)">
              <v-icon color="teal">edit</v-icon>
            </v-btn>
            <v-btn icon class="mx-0" @click="deleteItem(props.item)">
              <v-icon color="pink">delete</v-icon>
            </v-btn>
          </td>
        </tr>
      </template> -->
          <template v-slot:expanded-item="{ headers, item }">
            <td :colspan="headers.length">
              {{ item.Comentario }}
            </td>
          </template>
        </v-data-table>
      </v-flex>
    </v-layout>
  </v-container>
</template>

<script>
import TransFilter from "./TransFilter.vue";
import TransCreateEdit from "./TransCreateEdit";
import TransaccionesAPI from "../api/transacciones";
import { mapActions } from "vuex";

export default {
  name: "TransaccionesPage",
  components: {
    TransFilter,
    TransCreateEdit
  },
  data() {
    return {
      msg: "Lista Transacciones",
      search: "",
      headers: [
        { text: "Descripcion", value: "Descripcion", sortable: false },
        { text: "Concepto", value: "ConceptoId" },
        { text: "Cuenta", value: "CuentaId" },
        { text: "Cuenta Tranf.", value: "CuentaIdTransf" },
        { text: "Fecha", value: "Fecha" },
        { text: "Monto", value: "Monto", align: "right" },
        { text: "Actions", value: "action", sortable: false }
      ],
      pagination: {
        sortBy: ["Fecha"],
        sortDesc: true,
        itemsPerPage: 20
      },
      lista: [],
      filtroFechaDesde: this.$store.getters.firstDateString,
      filtroFechaHasta: this.$store.getters.todayDateString,
      filtroConcepto: 0,
      filtroCuenta: 0,
      dialog: false,
      transaccionId: 0,
      editedIndex: 0,
      loading: true
    };
  },
  methods: {
    ...mapActions(["setMessage", "setError"]),
    refresh() {
      this.$store.dispatch("incLoading");
      this.loading = true;
      TransaccionesAPI.getData(
        this.filtroFechaDesde,
        this.filtroFechaHasta,
        this.filtroConcepto,
        this.filtroCuenta
      )
        .then(response => {
          this.lista = response.data;
          this.$store.dispatch("decLoading");
          this.loading = false;
        })
        .catch(error => {
          this.$store.dispatch("decLoading");
          // alert(error)
          this.setError(error);
          this.loading = "error";
        });
    },
    newItem() {
      this.transaccionId = 0;
      this.dialog = true;
    },
    editItem(item) {
      this.editedIndex = this.lista.indexOf(item);
      this.transaccionId = item.TransaccionId;
      // this.editedItem = Object.assign({}, item)
      this.dialog = true;
    },
    deleteItem(item) {
      const index = this.lista.indexOf(item);
      if (confirm("Are you sure you want to delete this item?")) {
        TransaccionesAPI.deleteRecord(item)
          .then(() => {
            this.lista.splice(index, 1);
            this.refresh();
          })
          .catch(error => {
            this.setError(error);
          });
      }
    }
  },
  mounted() {
    this.refresh();
  }
};
</script>
