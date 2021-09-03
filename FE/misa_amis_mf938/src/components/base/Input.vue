<template>
    <!--<div class="input-wrapper">-->
        <input :class="[inputClass, { 'border-red': isBorderRed }]"
               :type="inputType"
               :placeholder="inputPlacehoder"
               :value="inputValue"
               :title="inputTitle"
               @input="[$emit('inputOnInput'), $emit('input', $event.target.value)]"
               @blur="$emit('inputOnBlur')"
               @keyup.enter="$emit('inputOnEnter')"
               ref="autofocus" />
        <!--<div class="input-title" v-if="inputTitle !== ''">{{inputTitle}}</div>-->
    <!--</div>-->
</template>


<script>
    export default {
        name: 'Input',
        props: {
            inputType: String,
            inputClass: String,
            inputPlacehoder: String,
            inputValue: String,
            inputTitle: {
                type: String,
                default: ''
            },
            isBorderRed: {
                type: Boolean,
                default: false
            },
            isFocus: {
                type: Boolean,
                default: false,
            },
        },
        watch: {
            //Nếu isFocus thay đổi thì thực hiện focus vào input this.
            isFocus() {
                this.$refs.autofocus.focus();
            },
        },
    };
</script>


<style scoped>


    /*.input-wrapper{
        width: auto;
        height: auto;
        box-sizing: border-box;
        position: relative;
    }

    .input-title{
        width: auto;
        height: auto;
        position: absolute;
        top: 20px;
        left: 24px;
        right: -50px;
        background-color: #111111;
        color: #fff;
        display: none;
        padding:16px;
        box-sizing:border-box;
        border-radius: 3px;
        z-index: 10000;
    }

    input:hover~.input-title{
        display:block;
    }

    input:focus ~ .input-title {
        display: none;
    }*/

    input {
        width: 240px;
        height: 32px;
        padding: 6px 10px;
        border-radius: 2px;
        outline: none;
        color: #111111;
        border: solid 1px #babec5;
        box-sizing: border-box;
        font-family: NotosansRegular;
        font-size: 13px;
    }

        input:focus {
            border-color: #2CA01C;
        }

        input.border-red {
            border-color: #FF0000;
        }

        input.input-search {
            padding: 6px 23px 6px 10px;
            width: 305px !important;
        }

            input.input-search::placeholder {
                font-family: NotosansItalic;
            }

    .input-w-12 {
        width: 836px;
    }

    .input-w-6 {
        width: 405px;
    }

    .input-w-4 {
        width: 239px;
    }

    .input-w-3 {
        width: 204px;
    }

    .input-w-2 {
        width: 160px;
    }
</style>