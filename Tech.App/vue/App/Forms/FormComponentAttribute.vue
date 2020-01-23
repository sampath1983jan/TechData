<template>
    <div>
        <div class="row">
            <div class="col-md-12" style="padding:0px;margin:0px;border-right:2px solid rgba(248, 248, 248, 0.84)">
                <div class=' list'>
                    <h5 class='' style="padding:5px; border-bottom:2px solid rgba(213, 205, 205, 0.29);font-weight:bold">Available Form Fields</h5><ul>
                        <li v-for="d in Attributes" class="dragAttribute ui-widget-content" :bind="d.FieldID"><div class=' rowlight'   style='padding-left:15px;'><a>{{d.AttributeName}}  </a></div>
                    </ul>
                </div>
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
    Attributes:this.attributes,
  }
        },
        props: ["appid","comp","formid","attributes"],
        created: function () {
    
        },
        mounted: function () {
            var that = this;
            $(".dragAttribute").draggable({ helper: "clone"});     
            $(".dropattribute").droppable({
                accept: ".dragAttribute",
                classes: {
                    "ui-droppable-active": "ui-state-active",
                    "ui-droppable-hover": "ui-state-hover"
                },
                drop: function (event, ui) {            
                  
                    var field = ui.draggable.attr("bind");                 
                    var item = that.Attributes.filter(function (x) { return x.FieldID == field });
                    item[0].container = ui.draggable;
                    item[0].container = ui.draggable;
                    that.$emit("addfield", item[0]);
                }
            });
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