<template>
    <div class="col-md-3">
 
            <div >
                <div class=' list' style='border-right:1px solid #ccc'>
                    <div class='stlabel'>Components</div><ul>
                        <li v-for="d in Comps"><div class=' rowlight' style='padding-left:15px;'><a v-on:click="ChangeAction(d.ComponentID)">{{d.ComponentName}} </a></div>
                    </ul>
                </div>
            </div>
        </div>
    </template>

<script>
     

    module.exports = {
        data: function () {
            return {
                Appid:"",
                Comps: [],
                renderComponent: this.refreshList,
            }
        },
        props: ["appid","refreshList"],
        created: function () {
            if (this.$route.params.appid == undefined) {
                this.Appid = this.appid;
            } else 
                this.Appid = this.$route.params.appid;
            
            this.getComp();
        },
        methods: {
            ChangeAction: function (eid) {              
                this.$emit('oncomponentchange',eid);
            }, forceRerender() {
                this.getComp();
            },
            getTop: function (no, data) {
              
                var i = 1;
                var d = [];
                if (no == -1) {
                    no = data.length;
                }
              
                $.each(data, function (i, v) {
                    if (i > no) {
                        return;
                    } else {
                        d.push(v.Component);
                        i = i + 1;
                    }
                });
               
                return d;
            }
            ,
            getComp: function () {
                var that = this;          
                if (localStorage.getItem('CompList') != null) {
                    this.Comps = this.getTop(-1, JSON.parse(localStorage.getItem('CompList')));
                } else {
                    $.ajax('/MyApp/' + this.Appid + '/Components',
                        {
                            type: "GET",
                            dataType: 'jsonp', // type of response data
                            data: {},
                            // timeout milliseconds
                            success: function (data, status, xhr) {  // success callback function         
                                var d = [];
                                d = that.getTop(-1, data);
                                localStorage.setItem('CompList', JSON.stringify(data));
                                that.Comps = data;
                            },
                            error: function (jqXhr, textStatus, errorMessage) { // error callback
                                alert(errorMessage);
                            }
                        });
                }
         
            }
        },
      
    }
</script>
