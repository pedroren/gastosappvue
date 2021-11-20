import Vue from "vue";
import Router from "vue-router";
import Home from "@/components/Home";
// import TransaccionesPage from '@/components/TransaccionesPage'
// import CuentasPage from '@/components/CuentasPage'
// import ConceptosPage from '@/components/ConceptosPage'
// import FacturasPage from '@/components/FacturasPage'
// import TransCreateEdit from '@/components/TransCreateEdit'
import SignIn from "@/components/SignIn";
// import FavoritosPage from '@/components/FavoritosPage'
// import MantenimientosPage from '@/components/MantenimientosPage'
// import MonedasPage from '@/components/MonedasPage'
// import PresupuestosPage from '@/components/PresupuestosPage'
// import ReportePage from '@/components/ReportePage'
// import RepConcepto from '@/components/RepConcepto'
// import RepCashFlow from '@/components/RepCashFlow'
// import RepHistConcepto from '@/components/RepHistConcepto'
// import MantImportPage from '@/components/MantImportPage'

Vue.use(Router);

export default new Router({
  routes: [
    {
      path: "/",
      name: "Home",
      component: Home
    },
    {
      path: "/login",
      name: "Login",
      component: SignIn
    },
    {
      path: "/transacciones",
      name: "Transacciones",
      // component: TransaccionesPage
      component: () =>
        import(
          /* webpackChunkName: "transacciones" */ "@/components/TransaccionesPage.vue"
        )
    },
    // {
    //   path: '/transacciones/new',
    //   name: 'Transaccion',
    //   component: TransCreateEdit
    // },
    {
      path: "/cuentas",
      name: "Cuentas",
      // component: CuentasPage
      component: () =>
        import(/* webpackChunkName: "cuentas" */ "@/components/CuentasPage.vue")
    },
    {
      path: "/mantenimientos",
      name: "Mantenimientos",
      // component: MantenimientosPage
      component: () =>
        import(
          /* webpackChunkName: "mantenimientos" */ "@/components/MantenimientosPage.vue"
        )
    },
    {
      path: "/mantenimientos/conceptos",
      name: "Conceptos",
      // component: ConceptosPage
      component: () =>
        import(
          /* webpackChunkName: "conceptos" */ "@/components/ConceptosPage.vue"
        )
    },
    {
      path: "/facturas",
      name: "Facturas",
      component: () =>
        import(
          /* webpackChunkName: "facturas" */ "@/components/FacturasPage.vue"
        )
      // component: FacturasPage
    },
    {
      path: "/mantenimientos/favoritos",
      name: "Favoritos",
      component: () =>
        import(
          /* webpackChunkName: "favoritos" */ "@/components/FavoritosPage.vue"
        )
      // component: FavoritosPage
    },
    {
      path: "/mantenimientos/monedas",
      name: "Monedas",
      component: () =>
        import(/* webpackChunkName: "monedas" */ "@/components/MonedasPage.vue")
      // component: MonedasPage
    },
    {
      path: "/mantenimientos/presupuestos",
      name: "Presupuestos",
      component: () =>
        import(
          /* webpackChunkName: "presupuestos" */ "@/components/PresupuestosPage.vue"
        )
      // component: PresupuestosPage
    },
    {
      path: "/mantenimientos/importaciones",
      name: "MantImport",
      component: () =>
        import(
          /* webpackChunkName: "importaciones" */ "@/components/MantImportPage.vue"
        )
      // component: MantImportPage
    },
    {
      path: "/reportes",
      name: "Reportes",
      component: () =>
        import(
          /* webpackChunkName: "reportes" */ "@/components/ReportePage.vue"
        )
      // component: ReportePage
    },
    {
      path: "/reportes/repconcepto",
      name: "RepConcepto",
      component: () =>
        import(
          /* webpackChunkName: "repconcepto" */ "@/components/RepConcepto.vue"
        )
      // component: RepConcepto
    },
    {
      path: "/reportes/repcashflow",
      name: "RepCashFlow",
      component: () =>
        import(
          /* webpackChunkName: "repcashflow" */ "@/components/RepCashFlow.vue"
        )
      // component: RepCashFlow
    },
    {
      path: "/reportes/histconcepto",
      name: "HistConcepto",
      component: () =>
        import(
          /* webpackChunkName: "histconcepto" */ "@/components/RepHistConcepto.vue"
        )
      //component: RepHistConcepto
    }
  ]
});
