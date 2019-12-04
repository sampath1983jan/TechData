 <template>   
     <div class="list">
         <div>             
             <ul>
                 <li v-for="d in Clients">
                     <div class='row rowlight'>
                         <div class='col-md-10'>
                             <router-link :to="d.url">
                                 {{d.ClientName}} - {{d.ClientNo}}
                             </router-link>
                         </div>
                         <div class='col-md-2'>
                             <a href="#" style="text-indent:-100%;" v-bind:name="d.ClientID">
                                 <span title="Delete" act="del" class="remove fa fa-remove"></span>
                             </a>
                         </div>
                     </div>
                 </li>
             </ul>
         </div>         
         <!--<router-link to="/foo">Go to Foo</router-link>
    <router-link to="/Bar"> Go to Bar</router-link>-->
         <router-view :key="$route.fullPath"></router-view>
     </div>
</template> 

 <script>      
     var mclient = {};
     //var View = require(['/vue/Client/clients'], function (client) {
     //    mclient = client;
     //    debugger;
     //});
    
     const View = {
         template: '<div>User {{ id }} {{ this.$route.params.action}}<div >{{Client.ClientName}}</div>  <div >{{Client.ClientNo}}</div>  <div >{{Client.ClientHost}}</div></div>  ',
         props: ['id'],
         data: function () {
             return {
                 Client: {}
             }
         },
         created() {
             this.getClient(this.$route.params.id);
         },
         methods: {
               
         getClient: function (id) {
                 var that = this;
                 $.ajax('/Client/Get',
                     {
                         type: "GET",
                         dataType: 'jsonp', // type of response data
                         data: { clientid: id},
                         // timeout milliseconds
                         success: function (data, status, xhr) {   // success callback function
                             that.Client = data;
                             console.log(that.Client);
                         },
                         error: function (jqXhr, textStatus, errorMessage) { // error callback
                             alert(errorMessage);
                         }
                     });
             }
         }
     }
  //   const Bar = { template: '<div>bar</div>' }
   //  const foo = { template: '<div>foo</div>' }

     const routes = [
       //  { path: '/vueclient/:id', component: vueclient },
         { path: '/View/:id/:action', component: View, props: true,},
         //{ path: '/Bar', component: Bar },
         //{ path: '/foo', component: foo },
     ]
     const router = new VueRouter({
         routes: routes// short for `routes: routes`
     });

     module.exports = {        
         data: function (){
             return {
                 Clients: []
             } 
         },

         created: function () {
             this.getClientList();
         },
         methods: {
             getClientList: function () {
                 var that = this;
                 $.ajax('/Client/Gets',
                     {
                         type: "GET",
                         dataType: 'jsonp', // type of response data
                         data: "",
                         // timeout milliseconds
                         success: function (data, status, xhr) {   // success callback function
                             that.Clients = data; 
                             $.each(that.Clients, function (i, v) {
                                 v.url = "/View/" + v.ClientID + "/v";
                             });
                         },
                         error: function (jqXhr, textStatus, errorMessage) { // error callback
                             alert(errorMessage);                             
                         }
                     });
             }
         },
         router: router
     }
 </script>


 