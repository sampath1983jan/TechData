
define(function () {     
    const vueclient = { template: '<div>User {{ $route.params.id }}</div>' };
    const Bar = { template: '<div>bar</div>' };
    const foo = { template: '<div>foo</div>' };
    const routes = [
        //{ path: '/vueclient/:id', component: vueclient },
        { path: '/vueclient/:id', component: vueclient },
        { path: '/Bar', component: Bar },
        { path: '/foo', component: foo }
    ];
    const router = new VueRouter({
        routes: routes// short for `routes: routes`
    });
    var c = "name: 'vueclient' ";        
    return Vue.component('Clients',
        {
            template: '<div class="list">'
                + ' <ul>'
                + ' <li v-for="d in Clients">'
                + "<div class='row rowlight'>"
                + "  <div class='col-md-10'>"
                + '<router-link  :to="{  '+ c +', params: { id: d.ClientID } }">'
                + '{{d.ClientName}} - {{d.ClientNo}} eeotkinh'
                + '</router-link>'
                + ' </div>'
                + " <div class='col-md-2'>"
                + '<a href="#" style="text-indent:-100%;" v-bind:name="d.ClientID">'
                + ' <span title="Delete" act="del" class="remove fa fa-remove"></span>'
                + '</a>'
                + '</div>'
                + ' </div>'
                + '  </li>'
                + '   </ul>         '
                + '  <router-link to="/foo">Go to Foo</router-link>'
                + '<router-link to="/vueclient/10001"> Go to Bar</router-link>'
                +" <router-view></router-view>"
                + '</div>'
            ,
            data: function () {
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
            },
            router: router
        });
});
