<template>
    <div class="row">
        <div class="col-md-12" >
            <div class="row" v-if="isNew==false">
                <div class="col-md-3" >
                    <div style="border-right: 1px solid #eee;">
                        <div class=' list'>
                            <div class=''><h6 style="float:left">Lookups</h6><a id="testing" style="margin-left:65%" v-on:click="showNew">
                                <i class="icofont-plus"></i></a></div>
                            <div>
                                <tzinput :attribute="{id:'txtlkSearch',isRequired:false,
            enabletooltip:true,min:0,inputType:2,max:500,limit:false}" v-on:Input="changeSearch"></tzinput>
                            </div>
                            <ul>
                                <li v-for="d in LookUps"><div class=' rowlight' style='padding-left:15px;' v-bind:class="{ active: d.isActive }"><a v-on:click="getLookUpItems(d.LookupID)">{{d.Name}} </a></div>
                            </ul>
                        </div>
                    </div>
                </div>
                <div class="col-md-8">
                    <div class="row">
                        <div class="col-md-12"><h5 style="float:left">{{Name}}</h5><a v-on:click="NewLookupItem">
    <i class="icofont-plus"></i>
</a></div>
                    </div>
                    <div class="row">
                        <div class="col-md-3">Name</div>
                        <div class="col-md-2">Short Name</div>
                        <div class="col-md-5">Description</div>
                        <div class="col-md-2">Is Active</div>
                    </div>
                    <div class="row" v-for="d in currentItems">
                        <div class="col-md-3">{{d.Label}}</div>
                        <div class="col-md-2">{{d.ShortLabel}}</div>
                        <div class="col-md-5">{{d.Description}}</div>
                        <div class="col-md-2">{{d.IsActive}}</div>
                    </div>
                </div>
                <div class="col-md-12" style="text-align:right">
                    <button class="btn btn-primary" value="Choose">Choose</button>
                    <button class="btn btn-default" value="Back" v-on:click="goBack">Back</button>
                </div>
            </div>
            <div class="row" >
                <div class="col-md-12" v-if="isNew">
                    <div v-if="isNewItem==false">
                        <add-lookup v-on:Added="afterAdded" v-on:cancelled="cancelled"></add-lookup>
                    </div>
                    <div v-else>
                        this is new items
                    </div>
                   
                </div>
            </div>       
        </div>      
    </div>    
</template>
<script>
     
    var addlk = httpVueLoader('/vue/App/Component/AddLookup.vue?' + (Math.random() * 10000));
 
    module.exports = {
        name: "Lookup",
        components: {
         "add-lookup": addlk
        },
        data: function () {
            return {
                Name:"LookUp Items",
                LookUps: [],
                currentItems: [],
                selected: "",
                isNew: false,
                Source: [],
                isNewItem:false,
            }
        },
    
        created: function () {          
            var that = this;
           
            this.getLookUps(function () {
                that.render();               
                $("#txtlkSearch").attr("placeholder", "Seach here");
            });
        },
        methods: {
            NewLookupItem: function () {
               
                this.isNew = true;
                this.isNewItem = true;
            },
            render: function () {



            },
            cancelled: function () {
              
                this.isNew = false;
            },
            afterAdded: function () {
                this.isNew = false;
            },
            showNew: function () {
                 
                this.isNew = true;
            },
            goBack: function () {
                 
                this.$emit("back");
            },
            changeSearch: function (val) {            
                if ($("#txtlkSearch").val() != "") {
                    var items = this.Source.filter(function (x) {
                        return (x.Name.toLowerCase().indexOf(val) >= 0);
                    });
                    this.LookUps = items;
                } else {
                    this.LookUps = this.Source;
                }                                   
            },
            getLookUpItems: function (lkitem) {
                this.selected = lkitem;       
                var that = this;
                $.each(this.LookUps, function (i, v) {
                    if (v.LookupID == lkitem) {
                        that.Name = v.Name;
                        v.isActive = true;
                    } else {
                        v.isActive = false;
                    }
                });
                var that = this;
                $.ajax({
                    url: '/ComponentManager/GetLookup',
                    type: "GET",
                    data: {
                        lookupid: lkitem
                    },
                    dataType: 'jsonp',
                    success: function (result) {                         
                        that.currentItems = result.LookupItems;
                       // renderLookupItems();
                    }
                });
            },
            getLookUps: function (callback) {
                var that = this;
                $.ajax({
                    url: '/App/' + appid +'/Lookup/get',
                    type: "GET",
                    data: {
                         
                    },
                    dataType: 'jsonp',
                    success: function (result) {                       
                        $.each(result.Lookups, function (i,v) {
                            that.LookUps.push(v.Lookup);
                            that.Source.push(v.Lookup);
                        })                    
                        callback();
                    }
                });
            }
        }
    }

    
</script>