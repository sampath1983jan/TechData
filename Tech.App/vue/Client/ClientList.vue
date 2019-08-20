 <template>
     <div class="list">
         <ul>
             <li v-for="d in Clients">
                 <div class='row rowlight'>
                     <div class='col-md-10'>
                         <a href='#' action='vwclient' v-bind:name='d.ClientID'>
                             {{d.ClientName}} - {{d.ClientNo}}
                         </a>
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
</template> 

 <script>      
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
                         },
                         error: function (jqXhr, textStatus, errorMessage) { // error callback
                             alert(errorMessage);                             
                         }
                     });
             }
         }
     }
 </script>