<template>
    <div class="row">
        <div class="col-md-12"  >
            <div class="row" v-if="isNew==false">
                <div class="col-md-11" style="margin:0px; padding:0px;">
                    <div class="row" style="border-bottom:1px solid #ddd;">
                        <div class="col-md-4" style="padding:0px;">
                            <div style="border-right: 1px solid #eee;">
                                <div class=' list'>
                                    <div class='' style="margin:5px 5px;">
                                        <h5 style="float:left;font-weight:500;">Lookups</h5><a id="testing" style="margin-left:65%" v-on:click="showNew">
                                            <i class="icofont-plus"></i>
                                        </a>
                                    </div>
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
                        <div class="col-md-8" style="padding:0px;">
                            <div class="row">
                                <div class="col-md-12" style="margin:5px 5px;border-bottom:1px solid #ddd;">
                                    <h5 style="float:left;font-weight:500;">{{Name}}</h5>
                                    <a style="float:right;cursor:pointer" v-if="IsCore==false" v-on:click="NewLookupItem">
                                        <i class="icofont-plus"></i>
                                    </a>
                                </div>
                            </div>
                            <!--<div class="row">
            <div class="col-md-3">Name</div>
            <div class="col-md-2">Short Name</div>
            <div class="col-md-5">Description</div>
            <div class="col-md-2">Value</div>
        </div>-->
                            <div class="row list">
                                <div class="col-md-12 list-group list-group-flush" style="border:0px;">
                                    <div class="list-group-item" style="padding:10px 5px;border:0px !important;" v-for="d in currentItems">
                                        <a class=" rowlight">
                                            <div class="circle" style="float:left;margin:5px; padding:5px 10px;">{{d.ShortLabel}}</div>
                                            <h6 class="list-group-item-heading">{{d.Value}}- {{d.Label}}     </h6>
                                            <div class="list-group-item-text">{{d.Description}}&nbsp;</div>
                                        </a>

                                    </div>
                                </div>




                            </div>
                        </div>
                    </div>
                </div>             
                <div class="col-md-11" style="text-align:right; padding-top:10px;">
                    <button class="btn btn-primary" value="Choose" v-on:click="choose">Choose</button>
                    <button class="btn btn-default" value="Back" v-on:click="goBack">Back</button>
                </div>
            </div>
            <div class="row" >
                <div class="col-md-12" v-if="isNew">
                    <div v-if="isNewItem==false">
                        <add-lookup v-on:Added="afterAdded" v-on:cancelled="cancelled"></add-lookup>
                    </div>
                    <div v-else>
                        <add-lookupitem :name="Name" :lkid="selected" v-on:added="itemadded" v-on:back="backtoform"></add-lookupitem>
                    </div>                   
                </div>
            </div>       
        </div>      
    </div>    
</template>
<script>
     
    var addlk = httpVueLoader('/vue/App/Component/AddLookup.vue?' + (Math.random() * 10000));
    var addlkitem = httpVueLoader('/vue/App/Component/AddLookupItem.vue?' + (Math.random() * 10000));
    module.exports = {
        name: "Lookup",
        components: {
            "add-lookup": addlk,
            "add-lookupitem":addlkitem
        },
        data: function () {
            return {
                Name: "LookUp Items",                
                LookUps: [],
                currentItems: [],
                selected: "",
                isNew: false,
                Source: [],
                isNewItem: false,
                IsCore:false,
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
                if (this.selected == "") {
                    return;
                }
                this.isNew = true;
                this.isNewItem = true;
            },
            choose: function () {
                this.$emit("selected", this.selected, this.Name);
            },
            render: function () {

            },
            itemadded: function () {
                this.isNewItem = false;
                this.isNew = false;
                this.getLookUpItems(this.selected);
            },
            backtoform: function () {
                this.isNewItem = false;
                this.isNew = false;
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
                        that.IsCore = v.IsCore;
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
                       
                        $.each(result.LookupItems, function (i, v) {
                            v.Index = i + 1;
                            if (v.ShortLabel == "") {
                                v.ShortLabel ="N/A"
                            }
                        });
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