
String.prototype.format = String.prototype.f = function () {
    var s = this,
        i = arguments.length;

    while (i--) {
        s = s.replace(new RegExp('\\{' + i + '\\}', 'gm'), arguments[i]);
    }
    return s;
};

define(function () {
    return Vue.component('toast',
        {
            template: '<div :style="setposition" :id="id"></div>',
            props: ["toas", "id","position","type"],   // type -linear nonlinear
            data: function () {
                return {    
                    toasts: this.toas,                    
                    template: '<div    id="{0}" class="toast" data-autohide="false">'
                        + '<div class="toast-header">'
                        + '<img src="..." class="rounded mr-2" alt="..." style="display:none">'
                        + '<strong class="mr-auto"></strong>'
                        + '<small></small>'
                        + '<button type="button" class="ml-2 mb-1 close">'
                        + ' <span aria-hidden="true">&times;</span>'
                        + '</button>'
                        + '</div>'
                        + ' <div class="toast-body">'                        
                        + ' </div></div>',
                    currentIndex:0,                  
                }
            },
            created: function () {

            },
            watch: {
                value: function (newVal, oldVal) { // watch it
                   
                }
            },
            mounted: function () {
                var othis = this;               
                this.$nextTick(function () {
                    if (this.type == "linear") {
                        if (this.toasts.length > 0) {
                            othis.render(0, this.toasts[0]);
                        }                        
                    } else {
                        $.each(this.toasts, function (it, itval) {
                            othis.render(it, itval);
                        });
                    }
                });
            },
            updated: function () {       
                var othis = this;           
                $.each(this.toasts, function (it, itval) {                
                    othis.push(itval);
                });                
                $('.toast').toast("show");    
            },            
            destroyed: function () {          
            },
            methods: {
                close: function (index) {
                 
                    var othis = this;
                    $("toz_" + index + "_" + this.id).click(function () {
                        othis.toasts.splice(index, 1);
                    });  
                },
                show: function () {
                    $('.toast').toast("show");    
                          
                },
                hide: function (idx) {
                    this.toasts.splice(idx, 1);
                }, 
                resetIndex: function () {
                    var othis = this;
                    $.each(this.toasts, function (idx, v) {
                        var id = "toz_" + idx + "_" + othis.id;
                        $("#" + id).find(".toast-header").find("[type=button]").attr("index", othis.toasts.length -1);
                    })
                },  
                render: function (index, item) {                   
                    var othis = this;
                
                    var id = "toz_" + index + "_" + this.id;
                  
                    var el = {};
                    if (item.template == "") {
                          el = $("#" + this.id).append(this.template.f(id));
                    } else {
                        el = $("#" + this.id).append(item.template.f(id));
                    }
                   
                    $("#" + this.id).css("z-index","100")
                    $("#" + id).find(".toast-body").append(item.body);
                    if (item.bodyCss != undefined) {
                        $("#" + id).find(".toast-body").addClass(item.bodyCss);
                    }
                    
                    $("#" + id).find(".toast-header").find("strong").append(item.header);
                    $("#" + id).find(".toast-header").find("[type=button]").attr("index", index);
                  //  $("#toz_" + index).toast("show");
                    if (item.autohide == true && item.header == "") {
                        $("#" + id).find(".toast-header").hide();
                    }
                    $("#" + id).animate({
                        opacity: "1"
                    }, 1000, "linear", function () {
                            var kthis = this;
                            
                            if (item.autohide == true) {
                                var inds = $(othis).attr("id").split[1];
                                othis.toasts.splice(index, 1);
                                othis.resetIndex();
                                setTimeout(function () {
                                    $(kthis).remove();
                                    if (othis.type == "linear") {
                                        if (othis.toasts.length > 0) {
                                            othis.render(0, othis.toasts[0]);
                                        }
                                    }
                                }, 3000);
                            }      

                            

                            //if (othis.type == "linear") {
                            //    if (othis.toasts.length > 0) {
                            //        othis.render(0, othis.toasts[0]);
                            //    }
                            //}     
                    });
                    $("#" + id).find(".toast-header").find("[type=button]").click(function () {
                        var index = $(this).attr("index");
                        othis.toasts.splice(index, 1);
                        othis.resetIndex();
                        $(this).parent().parent().animate({
                            opacity: "0"
                        }, 400, "linear", function () {
                                
                                var kthis = this;
                                $(kthis).remove();
                                if (othis.type == "linear") {
                                    if (othis.toasts.length > 0) {
                                        othis.render(0, othis.toasts[0]);
                                    }
                                }
                                                                                        
                              
                        });
                        //$(this).parent().parent().fadeOut(600, function () {
                        //    $(this).remove();
                        //});
                    }); 
                },
                push: function (item) {                    
                    var othis = this;                      
                    this.toasts.push(item);
                    if (this.type == "linear") {
                        if (this.toasts.length == 1) {
                            this.render(0, item);
                        }
                    } else if (this.type == "nonlinear") {
                        this.render(this.toasts.length -1, item);
                    }                
                     
                }                ,
                handleInput: function (e) {

                },
            },
            computed: {
                getToast: function () {
                    var othis = this;
                    $.each(this.toasts, function (i, v) {
                        v.template = othis.item;
                    });
                    return this.toasts;
                }
                ,
                setposition: function () {
                    var o = {};  
                    o.position = "fixed";
                    if (this.position == "top left" || this.position == "left top") {
                        o.left = "20px";
                        o.top = "60px";
                    } else if (this.position == "right top" || this.position == "top right") {
                        o.right = "20px";
                        o.top = "60px";
                    } else if (this.position == "bottom right" || this.position == "right bottom") {
                        o.bottom = "20px";
                        o.right = "20px";
                    } else if (this.position == "left bottom" || this.position == "bottom left") {
                        o.bottom = "20px";
                        o.left = "20px";
                    } else if (this.position == "center bottom" || this.position == "bottom center") {
                        o.bottom = "20px";
                        o.left = "40%";
                    } else if (this.position == "center top" || this.position == "top center") {
                        o.left = "40%";
                        o.top = "60px";
                    }else {
                        o.left = "80%";
                       
                    }
                    return o;
                }

            }


        });
});