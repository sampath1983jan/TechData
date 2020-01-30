<template>
    <div> 
        <h4>{{AppForm.Name}}</h4>
        <div class="row">
            <div class="col-md-2">
                <div id="compAttribute"></div>
            </div>
            <div class="col-md-7 dropattribute">             
                <div class="grid-stack"></div>
            </div>
            <div class="col-md-3" id="fattribute"  >

            </div>
        </div>
       
    </div>   
    
</template>
<script>
    var jPopupDemo = {};
    module.exports = {
        name: "FormDeisgner",        
        data: function () {
            return {
                FormID: "",
                AppID: "",
                componentTemplate: "<div id='chComp'><div><choose-component :appid='AppID' :formid='FormID' :comp='Component'></choose-component></div></div>",
                componentAttributeTemplate: "<div><component-attribute :attributes='ComponentAttribute' v-on:addfield='NewField'></component-attribute></div>",
                attributeTemplate:"<div id='{0}'><attribute-property :attribute='attribute' :formfieldattribute='formfieldattribute' :rendertype='rendertype' :attributename='attributename' :fieldid='fieldid' :formid='FormID' v-on:save='addattribute' v-on:back='goback'></attribute-property></div>",
                AppForm: {},
                FormFields: [],
                Designer: {},
                Node: {},
            }
        },
        beforeRouteEnter(to, from, next) {         
             
            next();
        },
        beforeRouteUpdate(to, from, next) {    
            
            next();
        },
        props: ["name"],
        created: function () {             
            this.AppID = this.$route.params.id;
            this.FormID = this.$route.params.fid;
            this.getForm(this.SetComponent);
        },
        mounted: function () {
            var that = this;
            $('.grid-stack').gridstack({ animate: true, cellHeight: 40, verticalMargin: 5, resizable: { autoHide: true, handles: "e" } });       
            $('.grid-stack').data('gridstack').setAnimation(true);
            var nodes = [];          
        

            this.Designer = new DesignGrid(nodes);
            this.Designer.afterAdded = function (node) {                 
                that.Node = node;
            }
            this.Designer.afterRemove = function (node) {           
                that.Node = node;
                var ff = [];
                $.each(that.FormFields, function (i, n) {
                    var f = that.Node.filter(function (x) {
                        return x.FieldID == n.id || x.FormFieldID == n.id;
                    });                    
                    if (f.length == 0) {
                        ff.push(n);
                    }  
                    that.FormFields = ff;
                 //   debugger;
                });   
            }
            this.Designer.afterChange = function (node) {
                that.Node = node;                 
                var fposition = [];
                $.each(node, function (i, n) {
                    var f = that.FormFields.filter(function (x) {
                        return x.Attribute.FieldAttribute.FieldID == n.id || x.FormFieldID== n.id;
                    });                     
                    if (f.length > 0) {
                        f = f[0];
                    } else {
                        f = {};
                    }
                    f.Top = n.y;
                    f.Left = n.x;
                    f.Width = n.width;
                    f.Height = n.height;
                    
                    var tt = {};
                    tt.Top = n.y;
                    tt.Left = n.x;
                    tt.Width =n.width;
                    tt.Height = n.height;
                    tt.FieldID = n.id;
                    fposition.push(tt);
                });    
                $.ajax('/App/' + that.AppID + '/Form/changeshape/' + that.FormID,
                    {
                        type: "GET",
                        dataType: 'jsonp', // type of response data
                        data: { fieldPosition: JSON.stringify(fposition) },
                        // timeout milliseconds
                        success: function (data, status, xhr) {  // success callback function

                    
                        },
                        error: function (jqXhr, textStatus, errorMessage) { // error callback
                            alert(errorMessage);
                        }
                    });   
            }
            this.Designer.itemSelected = function (node) {                 
                var ele = that.FormFields.filter(function (x) {
                    return x.Attribute.FieldAttribute.FieldID == node;
                });
           

                that.AttributeFormRender(ele[0].Attribute.FieldAttribute, ele[0].Attribute.Category, ele[0])
            }
        },
        methods: { 
            goback: function () {
                jPopupDemo.Close();
            },        
            AttributeFormRender: function (element, frt, formfield) {
                $("#fattribute").html("");
                $("#fattribute").append(this.attributeTemplate.format("abcd"));
                this.AttributeForm("abcd", element, frt, formfield);
            },
            AttributeForm: function (plc, element, frt, formfield) {
                
                var that = this;
                var aProperty = httpVueLoader('/vue/App/Forms/AttributeProperties.vue?' + (Math.random() * 10000));
                var self = new Vue({
                    components: {
                        'attribute-property': aProperty,
                    },
                    data: function () {
                        return {
                            AppID: that.AppID,
                            attribute: element,
                            formfieldattribute:formfield,
                            FormID: that.FormID,
                            rendertype: frt,
                            attributename: element.AttributeName,
                            fieldid: element.FieldID,
                        }
                    },
                    methods: {
                        Save: function (fld) {

                        },
                        addattribute: function (fld) {
                            if (formfield == undefined) {
                                that.Designer.addNewWidget(element);
                                fld.Top = that.Node.y;
                                fld.Left = that.Node.x;
                                fld.Width = that.Node.width;
                                fld.Height = that.Node.height;
                            }
                            
                            $.ajax('/App/' + that.AppID + '/Form/createfield/' + that.FormID,
                                {
                                    type: "GET",
                                    dataType: 'jsonp', // type of response data
                                    data: { renderType: fld.RenderType, attributeID: fld.FieldID, formelement: JSON.stringify(fld) },
                                    // timeout milliseconds
                                    success: function (data, status, xhr) {  // success callback function
                                        if (data.FormFieldID == "") {
                                            that.Designer.Remove(fld.FieldID);

                                        } else {
                                            if (formfield == undefined) {
                                                that.FormFields.push(data);
                                            }
                                        }                                                                    
                                                                  

                                        jPopup.prototype.close(true);
                                    },
                                    error: function (jqXhr, textStatus, errorMessage) { // error callback
                                        alert(errorMessage);
                                    }
                                });



                        },
                        goback: function () {
                            jPopup.prototype.close(true);
                        },
                    }
                }).$mount('#'  + plc);
            },
            NewAttributeForm: function (element) {
                var that = this;
                var jPopupDemo = new jPopup({
                    content: this.attributeTemplate.format("attProperty"),
                    hashtagValue: '#demopopup',
                    shouldSetHash: false
                });
                $(".jPopup").find(".content").attr("id", "newelement");
                $(".jPopup").find(".content").css("top", "35%");
                $(".jPopup").find(".content").css("width", "90%");
                $(".jPopup").find(".content").css("padding-left", "10%");
                that.AttributeForm("attProperty", element,0);
                               
            },
            SetComponent: function () {  
                var that = this;
                if (this.AppForm.ComponentID == "") {
                    //  $("#newElemt").click(function (e) {                     
                    var jPopupDemo = new jPopup({
                        content: this.componentTemplate,
                        hashtagValue: '#demopopup',
                        shouldSetHash: false
                    });
                    $(".jPopup").find(".content").attr("id", "newelement");
                    $(".jPopup").find(".content").css("top", "40%");
                    $(".jPopup").find(".content").css("width", "90%");
                    $(".jPopup").find(".content").css("padding-left", "10%");
                    //});
                    that.getComp(function (data) {
                         
                        var chComp = httpVueLoader('/vue/App/Forms/ChooseComponent.vue?' + (Math.random() * 10000));
                        var self = new Vue({
                            components: {
                                'choose-component': chComp
                            },
                            data: function () {
                                return {
                                    AppID: that.AppID,
                                    Component: data,
                                    FormID: that.FormID,
                                }
                            },
                            methods:  {
                               
                            },
                        }).$mount('#chComp');
                    });
                }
                else {                  
                    $.each(this.AppForm.FormFields, function (i, v) {
                        var node = {
                            x: v.Attribute.Left,
                            y: v.Attribute.Top,
                            width: v.Attribute.Width,
                            height: v.Attribute.Height,
                            id: v.Attribute.FieldAttribute.FieldID
                        };
                     
                        that.Designer.addNewWidget(v.Attribute.FieldAttribute, node);
                    });
                    $("#compAttribute").append(this.componentAttributeTemplate)
                    var compAttri = httpVueLoader('/vue/App/Forms/FormComponentAttribute.vue?' + (Math.random() * 10000));
                    that.getComp(function (data) {                     
                        var self = new Vue({
                            components: {
                                'component-attribute': compAttri
                            },
                            data: function () {
                                return {
                                    AppID: that.AppID,
                                    ComponentAttribute: data.Attributes,
                                    FormID: that.FormID,
                                }
                            },
                            methods: {
                                addattribute: function () {

                                },
                                NewField: function (element) {   
     
                                    var exist = that.FormFields.filter(function (x) { return x.Attribute.FieldAttribute.FieldID == element.FieldID });                                     
                                    if (exist.length > 0) {
                                        Window.toz.$refs.foo.push({
                                            header: "", body: "Field already exist in this form", template: ""
                                            , autohide: true,
                                            bodyCss: "feedback",
                                            position: "top right",
                                            autohide: true,
                                        });
                                        return;
                                    }                                    
                                    that.NewAttributeForm(element);
                                },
                            }
                        }).$mount('#compAttribute');
                    })
                    
                }
            },
            getForm: function (callback) {
                var cForm = this;               
                    $.ajax('/App/' + this.AppID + '/Form/Get/' + this.FormID,
                        {
                            type: "GET",
                            dataType: 'jsonp', // type of response data
                            data: {},
                            // timeout milliseconds
                            success: function (data, status, xhr) {  // success callback function
                                cForm.AppForm = data;
                                cForm.FormFields = cForm.AppForm.FormFields;
                                cForm.Name = cForm.AppForm.Name;
                                $("#apptitle").find("h5").css({ "float": "left" });
                                $("#apptitle").find("h5").html("/" + cForm.AppForm.Name);
                                callback();
                            },
                            error: function (jqXhr, textStatus, errorMessage) { // error callback
                                alert(errorMessage);
                            }
                        });              

            },
            getComp: function (callback) {
                var that = this;
                $.ajax('/App/' + this.AppID + '/Component/' + this.AppForm.ComponentID,
                    {
                        type: "GET",
                        dataType: 'jsonp', // type of response data
                        //  data: { clientid: id },
                        // timeout milliseconds
                        success: function (data, status, xhr) {   // success callback function    
                      
                            callback(data);
                         
                        },
                        error: function (jqXhr, textStatus, errorMessage) { // error callback
                            alert(errorMessage);
                        }
                    });

            }
        } 
        
    }  

    function DesignGrid(ff) {
        this.afterAdded = function (v) {

        }
        this.afterRemove = function (v) {

        }
        this.afterChange = function () {

        }
        this.itemSelected = function (item) { }
        
        this.items = ff;
        var count = 0;
        var that = this;
        this.grid = $('.grid-stack').data('gridstack');
        $('.grid-stack').on('change', function (event, items) {             
            that.afterChange(items);
        });     
        this.eventSelect = function (ids) {
            var that = this;            
         var items = $('.grid-stack').find("[data-gs-id='"+ ids +"']");         
            $(items).click(function () {
                var ids = $(this).attr("data-gs-id");

              
                var it = that.items.filter(function (x) {
                    return x.id == ids;
                })
                that.itemSelected(it[0].id)
            })       
        }
        this.Remove = function (fid) {
            var newitem = [];
            $.each(this.items, function (i, v) {
                if (v.FieldID != fid) {
                    newitem.push(v);
                }
            });
            this.items = newitem;
             
            $widget = $(".grid-stack").find("[data-gs-id=" + fid + "]");
           
            this.grid.removeWidget($widget);
            this.afterRemove(this.items);
        }
        this.addNewWidget = function (ele, nd)
        {
            
            var node = {};
            if (nd == undefined) {
                node = {
                    x: 0,
                    y: this.items.length,
                    width: 3,
                    height: 1,
                    id: ele.FieldID
                };             
                this.grid.addWidget($('<div><div class="grid-stack-item-content"><div style="width:90%;float:left" >' + ele.AttributeName + '</div><a id="a_' + ele.FieldID + '" class="icofont-close-line" style="display:none"></a></div></div>'), node);
                this.afterAdded(node);
            } else {
                node = nd;
                this.grid.addWidget($('<div><div class="grid-stack-item-content"><div style="width:90%;float:left" >' + ele.AttributeName + '</div><a id="a_' + ele.FieldID + '" class="icofont-close-line" style="display:none"></a></div></div>'), node);

            }          
            this.items.push(node);
 
            $(".grid-stack").find("[data-gs-id=" + ele.FieldID + "]").find("a").click(function () {
                var fid = this.id.split("_")[1];
                
                that.Remove(fid,this);
            });
            $(".grid-stack-item-content").mouseover(function () {             
                $(this).find("a").show();
            });
            $(".grid-stack-item-content").mouseout(function () {
                $(this).find("a").hide();
            });
       
            this.eventSelect(ele.FieldID);
            return false;
        }.bind(this);
   //     this.addNewWidget();
        this.toggleFloat = function () {
            this.grid.float(!this.grid.float());
            $('#float').html('float: ' + this.grid.float());
        }.bind(this);
        //this.addNewWidget();
        //this.addNewWidget();
        //$('#add-widget').click(this.addNewWidget);
        //$('#float').click(this.toggleFloat);
    };

    </script>
