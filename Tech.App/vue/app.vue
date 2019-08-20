<template>
    <div>
        <h2>Vue + RequireJS demo (jp-d)</h2>
        <ul>
            <li v-for="i in items">
                {{i}}
            </li>
        </ul>
        <div id="dynamic-placeholder">
            <a href="#" @click.prevent="loadAnother">load dynamic component</a>
        </div>
        <div id="comp1">
            <blog-post v-bind:tdate="date" title="sampath kumar post"></blog-post>
        </div>
    </div>    
</template>
<style>
    /* app's stylesheet */
    body {
        background: grainsboro;
        color: chucknorris;
    }
    li {
        list-style-type: cjk-ideographic;
    }
</style>
<script type="text/javascript">
    // require Vue
    define(['Vue'], function (Vue) {
        // create and return app instance
        // refer template as `template`
        return new Vue({
            template: template,
            data: {
                // sample data: Japanese numbers
                items: [
                    'ichi',
                    'ni',
                    'san',
                    'shi',
                    'go',
                    'roku',
                    'shichi',
                    'hachi'
                ],
                date: new Date()
            },
            methods: {
                loadAnother: function () {
                    var ss = this;
                    //import Pixel from 'vue/FileReader'
                    //alert(Pixel);
                    // example of dynamic Vue loading
                    //require(['vue!/vue/FileReader'], function (compo) {
                    //    var ComponentClass = Vue.extend(compo)
                    //  ////  alert(ComponentClass);
                    //    var instance = new ComponentClass()   
                    //    instance.$mount()
                    //    //  $("#comp").append(instance.$el)
                    //    //$("#comp1").append('<blog-post v-bind: tdate="date" title="sampath kumar post"></blog-post>');

                    //    var a = new Vue({
                    //        el: "#comp1",
                    //        components: {
                    //            compo
                    //        },
                    //        data: {
                    //            date: 'Hello Vue!'
                    //        }
                            
                    //    });
                    //})
                }
            }
        })
    });
</script>