<template>
    <div class="row">
        <div class="col-md-3">
            <div>
                <div class=' list' style='border-right:1px solid #ccc'>
                    <div class='stlabel'>Lookups</div><ul>
                        <li v-for="d in LookUps"><div class=' rowlight' style='padding-left:15px;'><a v-on:click="getLookUpItems(d.LookUpID)">{{d.Name}} </a></div>
                    </ul>
                </div>
            </div>
        </div>
        <div class="col-md-8">

        </div>
    </div>
    
</template>

<script>
    module.exports = {
        name: "Lookup",
        components: {

        },
        data: function () {
            return {
                LookUps: [],
                currentItems:[],
            }
        },
    
        created: function () {
             
            var that = this;
            this.getLookUps(function () {
                that.render();
            });
        },
        methods: {
            render: function () {

            },
            getLookUpItems : function (lkitem) {
                $.ajax({
                    url: '/ComponentManager/GetLookup',
                    type: "GET",
                    data: {
                         lookupid: lookupid
                    },
                    dataType: 'jsonp',
                    success: function (result) {
                        currentItems = result.LookupItems;
                        renderLookupItems();
                    }
                });
            },
            getLookUps: function (callback) {
                var that = this;
                $.ajax({
                    url: '/ComponentManager/GetLookups',
                    type: "GET",
                    data: {
                         
                    },
                    dataType: 'jsonp',
                    success: function (result) {                         
                        that.LookUps = result;
                        callback();
                    }
                });
            }
        }
    }

    
</script>