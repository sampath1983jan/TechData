<template>
    <div>
        <div class="row">
            <div class="col-md-3" style="padding:0px;margin:0px;border-right:2px solid rgba(248, 248, 248, 0.84)">
                <div class=' list'>
                    <h4 class='' style="padding:5px; border-bottom:2px solid rgba(213, 205, 205, 0.29);font-weight:bold">Attribute list</h4><ul>
                        <li v-for="d in attri.Attributes"><div class=' rowlight' style='padding-left:15px;'><a v-on:click="ShowAttribute(d.FieldID)">{{d.AttributeName}} </a></div>
                    </ul>
                </div>
            </div>
            <div class="col-md-9" style="padding:0px;margin:0px;background-color:#fff">
                <router-view :key="$route.fullPath" :onAdd="AfterAdd"></router-view>
            </div>
        </div>
    </div>   
</template>

<script type="text/javascript">
    module.exports = {
        name: "FormComponentAttribute",
        components: {

        },
        data: function () {
            return {
    AppID:this.appid,
    ComponentID:this.comp,
    FormID:this.formid,
    Attributes:[],
            }
        },
        props: ["appid","comp","formid"],
        created: function () {
        this.getAttributes();
        },
        methods: {

     getAttributes: function () {
                var that = this;
                $.ajax('/App/' + this.AppID + '/Component/' + this.ComponentID,
                    {
                        type: "GET",
                        dataType: 'jsonp', // type of response data
                        success: function (data, status, xhr) {   // success callback function
                            that.Attributes = data;
                        },
                        error: function (jqXhr, textStatus, errorMessage) { // error callback
                            alert(errorMessage);
                        }
                    });
            },
            getCompFields: function () {
                var that = this;

                $.ajax('/MyApp/' + this.AppID + '/Components',
                    {
                        type: "GET",
                        dataType: 'jsonp', // type of response data
                        data: {},
                        // timeout milliseconds
                        success: function (data, status, xhr) {  // success callback function
                            var d = [];
                            $.each(data,function (i,v) {
                                d.push(v.Component);
                            })
                            that.Comps = data;
                        },
                        error: function (jqXhr, textStatus, errorMessage) { // error callback
                            alert(errorMessage);
                        }
                    });
            }

        },
        //router: router
    }
</script>