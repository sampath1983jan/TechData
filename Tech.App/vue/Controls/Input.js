
define(function () {
    var setTemplate = function (itype, ele, attr) {
        itype = parseInt(itype);
     
        if (itype === 1 || itype === 2 || itype === 3 || itype === 4 || itype === 5 || itype === 6 || itype === 13) {
            var d = document.createElement("div");
            $(ele).find('input').addClass("form-control");
            $(d).attr("data-toggle", "tooltip");
            $(d).attr("title", "this is required field");
            $(ele).find('input').appendTo(d);
          
            var hlp = "";
            if (attr.note !== undefined) {
                hlp = attr.note;
            }
               if (attr.limit === true) {
              hlp = hlp + " " + "(Length min-" + attr.min + " max-" + attr.max + ")";
               }
            $(d).append('<small id="emailHelp" class="form-text help-text-muted">' + hlp +  '</small>');
            $(ele).append(d);
        }
        else if (itype == 7 || itype == 9) { // datepicker
            var d = document.createElement("div");
            $(d).attr("data-toggle", "tooltip");
            $(d).attr("title", "this is required field");
            $(ele).find('input').appendTo(d);
            //$(d).append(' <i class="fa fa-calendar"></i>');
            $(ele).append(d);
        }
        else if (itype == 10 || itype == 11) {

            //var d = document.createElement("div");
            // $(d).attr("data-toggle", "tooltip");
            // $(d).attr("title", "this is required field");
            //$(ele).find('input').appendTo(d);
            $(ele).find('input').addClass("form-control option-input checkbox");
            //  if (prt.limit === true) {
            // var  hlp = hlp + " " + "(Length min-" + 0 + " max-" + 100 + ")";
            //  }
            ///$(d).append('<small id="emailHelp" class="form-text help-text-muted">' + hlp + '</small>');

        } else if (itype == 12) {
            $(ele).find('input').addClass("form-control");
            $(ele).find('input').attr("multiple", "");
            var d = document.createElement("div");
            $(d).attr("data-toggle", "tooltip");
            $(d).attr("title", "this is required field");
            var l = document.createElement("label");
            $(l).addClass("file-upload btn btn-primary");
            $(l).html("Browse for file..");
            $(d).append(l);
            $(ele).find('input').appendTo(l);
            $(ele).append(d);
        }
        else if (itype === 15) {
            var d = document.createElement("div");
            $(d).addClass("input-group colorpicker-component");
            $(ele).find('input').appendTo(d);
            $(d).append('<span class="input-group-addon"><i></i></span>');
            $(ele).append(d);
        } else if (itype == 16) {
            var d = document.createElement("div");
            $(d).attr("data-toggle", "tooltip");
            $(d).attr("title", "this is required field");
            var l = document.createElement("label");
            $(l).addClass("switch");
            $(l).css("margin-bottom", "0px");
            $(d).append(l);
            $(ele).find('input').appendTo(l);
            $(l).append("<span class='sbslider round'></span>");
             $(ele).append(l);

        }
    }
    return Vue.component('tzinput',
        {
            template: '<div><input :type="getType"  v-model="getValue"  @blur="handleInput"  v-on:change="changeinput"   @input="handleInput"/></div>'
            ,
            props: ['value', 'type', 'inputType', 'attribute','req', 'id'],
            data: function () {
                return {
                    required: true,                  
                    length: 200,
                    with: 200,
                    height: '',
                    text: this.value,          
                }
            },
            created: function () {

            },
            watch:  {
               
            },             
            mounted: function () {
                var othis = this;              
                this.$nextTick(function () {
                    setTemplate(this.attribute.inputType, this.$el, this.attribute);
                    $(this.$el).find("input").Input(this.attribute);                    
                    $(this.$el).find("input").Input("text", this.text);
                    $(othis.$el).find('input[type=checkbox]').click(function () {
                        if ($(this).prop("checked") == true) {
                            othis.text = true;
                            othis.$emit("input", othis.text);
                        }
                        else if ($(this).prop("checked") == false) {
                            //alert("Checkbox is unchecked.");
                            othis.text = false;
                            othis.$emit("input", othis.text);
                        }
                    });
                    $(othis.$el).find('input').keypress(function () {
                        othis.text = $(this).val();
                        othis.$emit("text", othis.text);
                    });
                    $(othis.$el).find('input').blur(function () {
                        //othis.text = $(this).val();
                        //othis.$emit("text", othis.text);
                    });
                    $(othis.$el).find('input').change(function () {
                        if (othis.attribute.inputType == 5) {
                            othis.text = $(this).timepicki("getValue").date;
                            othis.$emit('input', $(this).timepicki("getValue"));
                        } else if (othis.attribute.inputType == 13) {
                            othis.text = $(this).val();
                            othis.$emit('input', othis.text);
                        }                     
                       // alert($(this).timepicki("getValue"));                 
                    });
                    $(othis.$el).find('input').css({ "border": "1px solid #aaa", "border-radius": "5px" });
                });
            },
            updated: function () {
            //    $(this.$parent.$el).find('input').css("border", "1px solid black");
            //    alert($(this.$parent.$el).html());

            },
            destroyed: function () {
             //   $(this.$parent.$el).find('input').css("border", "1px solid black");
            //    alert($(this.$parent.$el).html());
            },
            methods: {
                gettime: function () {
                    var ti = 0;
                    var mi = 0;
                    var mer = "AM";
                    var d = {};
                    if (this.text instanceof Date) {
                        d = this.text;
                    } else if (!isNaN(Date.parse(this.text))) {
                        d = Date.parse(this.text);
                    } else {

                        return "";
                    }
                    ti = d.getHours();
                    mi = d.getMinutes();
                    mer = "AM";
                    if (this.text.indexOf("AM") > 0 || this.text.indexOf("PM") >0) {
                        if (ti === 0) { // midnight
                            ti = 12;
                        } else if (ti === 12) { // noon
                            mer = "PM";
                        } else if (ti > 12) {
                            ti -= 12;
                            mer = "PM";
                        }
                    }
                    return [ti, mi, mer];

                },
                handleInput: function (e) {    
                   // console.log(this.text);
                    this.$emit('input', this.text);
                    this.$emit('handle-Input', this.text);
                },
                changeinput: function (e) {
                 //   alert(this.text);
                   console.log(this.text);
                    this.$emit('input', this.text);
                },
                setValue: function (value) {
                    this.height = value;
                },
                addStyle: function () {

                },
                isrequired: function () {
                    if (this.required == true) {
                        return 'required';
                    }

                },
                getType: function () {
                    if (this.attribute.inputType == "number") {
                        return "date";
                    }
                }
            },
            computed: {
                fullName:{
                    // getter
                    get: function () {
                        return this.firstName + ' ' + this.lastName;
                    },
                    // setter
                    set: function (newValue) {
                        var names = newValue.split(' ');
                        this.firstName = names[0];
                        this.lastName = names[names.length - 1];
                    }
                },
                getValue: {
                    // getter
                    get: function () {
                    
                        if (this._props.attribute.inputType == 5) {
                            return this.gettime();
                        } else {
                        return this.text;
                        }
                      //  return this.text;
                    },
                    // setter
                    set: function (newValue) {
                        this.text = newValue
                    }
                },
                getType: {
                    get: function () {                       
                        var txt = "text";
                        if (this.attribute.inputType == 1) { //textbox
                            txt = "text";
                        } else if (this.attribute.inputType == 2) { //password
                            txt = "text";
                        }
                        if (this.attribute.inputType == 10 || this.attribute.inputType==16) { // checkbox
                            txt = "checkbox";
                        } else if (this.attribute.inputType == 11) { // radio
                            txt = "radio";
                        } else if (this.attribute.inputType == 12) {
                            txt = "file";
                        }
                        return txt;
                    },
                    set: function() {

                    }
                  
                },
                getTemplate: {
                    get: function () {
                        if (this.attribute.inputType == 8) {
                            return '<textarea rows="5"  v-model="text" id="" class="form-control" placeholder=""/>';
                        } else {
                            return '';
                        }
                       
                    },
                    set: function() { }
                }
            }
            // router: router
        });
});




