<template>
    <div class="col-md-3">

        <div>
            <div class=' list' style='border-right:1px solid #ccc'>
                <div class='stlabel'>Forms</div><ul>
                    <li v-for="d in Forms"><div class=' rowlight' style='padding-left:15px;'><a v-on:click="ChangeAction(d.FormID)">{{d.Name}} </a></div>
                </ul>
            </div>
        </div>
    </div>
</template>
<script>    
    module.exports = {
        data: function () {
            return {
                Appid: "",
                Forms: [],
                renderComponent: this.refreshList,
            }
        },
        props: ["appid"],
        created: function () {
            if (this.$route.params.appid == undefined) {
                this.Appid = this.appid;
            } else
                this.Appid = this.$route.params.appid;
            this.getForms();
        },
        methods: {
            ChangeAction: function (eid) {
                this.$emit('onformselection', eid);
            },
            forceRerender() {
                this.getForms();
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
                        d.push(v.Form);
                        i = i + 1;
                    }
                });
                return d;
            }
            ,
            getForms: function () {
                var that = this;
                if (localStorage.getItem('FormList') != null) {
                    this.Forms = this.getTop(-1, JSON.parse(localStorage.getItem('FormList')));
               
                } else {
                    $.ajax('/App/' + this.Appid + '/Form/Get',
                        {
                            type: "GET",
                            dataType: 'jsonp', // type of response data
                            data: {},
                            // timeout milliseconds
                            success: function (data, status, xhr) {  // success callback function
                                var d = [];
                               
                                d = that.getTop(-1, data.Forms);
                              //  localStorage.setItem('FormList', JSON.stringify(data));
                                that.Forms = d;
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
