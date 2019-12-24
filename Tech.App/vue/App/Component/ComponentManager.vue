<template id="ComponentManager">
    <div id="appView">   
        <div>sampathkumar</div>
        <comp-attribute></comp-attribute>
               <!--<router-view :key="$route.fullPath"></router-view>-->        
    </div>
</template>
<script>
    var moment = {};
    var a = httpVueLoader('/vue/App/Component/ComponentList.vue?3' + (Math.random() * 10000));
    var CompForm = httpVueLoader('/vue/App/Component/CompForm.vue?' + (Math.random() * 10000));  
    var fmlist = httpVueLoader('/vue/App/Forms/FormList.vue?' + (Math.random() * 10000));
    var attributes = httpVueLoader('/vue/App/Component/CompAttributes.vue?' + (Math.random() * 10000));

    var cdetail = {};
    var myComp = function (next,fn) {
        var sthis = {};       
       // this._data.title = "sampathkumar";
        $.ajax('/App/' + this.AppID + '/Component/' + this.CompID,
            {
                type: "GET",
                dataType: 'jsonp', // type of response data            
                success: function (data, status, xhr) {   // success callback function    
                    cdetail = data;                  
                    next(fn);
                },
                error: function (jqXhr, textStatus, errorMessage) { // error callback
                    alert(errorMessage);
                }
            });
    } 
    module.exports = {
        name: "componentmanager",
        components: {
            'comp-list': a,
            'comp-form': CompForm,
            'form-list': fmlist,
            'comp-attribute': attributes
        },
        data: function () {
            return {
                Appid: this.id,
                Complist: [],
                refreshList: true,
                compid: "",
            }
        }, beforeRouteEnter(to, from, next) {
            //debugger;
            //if (to.name == "component") {
            //    $("#itemlist").hide();
            //}
            next();
           
        }, beforeRouteUpdate(to, from, next) {          
            //if (to.name == "component") {
            //    $("#itemlist").hide();
            //} else if (to.name == "home" || to.name == undefined) {
            //    $("#itemlist").show();
            //}
            next();
        },
        props: ["name"],
        created: function () {            
            this.compid = this.$route.params.cid;                 
        },
        methods: {         
            showForm: function () {               
                this.$router.push({ path: "/foo", params: { id: this.Appid, cid: this.compid, rd: + Math.random() * 100001 }});    
            },                    
            refresh: function () {
                if (this.refreshList == true) {
                    this.refreshList = false;
                }
                else this.refreshList = true;
            }
        },
        //router: router
    }

</script>