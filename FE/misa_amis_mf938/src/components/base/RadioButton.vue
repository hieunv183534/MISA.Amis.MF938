<template>
        <button class="radio-button" @click="$emit('radioButtonOnClick')" :class="{'radio-button-active': rbValue}">
            <div v-if="rbValue" class="radio-button-check"></div>
            <div class="radio-button-focus"></div>
        </button>
</template>


<script>
export default {
    name:'Name',
    props:{
        rbValue: {
            type: Boolean,
            default: false
        }
    },
}
</script>


<style scoped>
    
    .radio-button{
        padding:0;
        margin:0;
        outline:0;
        border:0;
        background-color:#fff;
        width: 18px;
        height: 18px;
        border-radius: 50%;
        border: 1px solid #AFAFAF;
        display: flex;
        justify-content: center;
        align-items: center;
        box-sizing: border-box;
        position: relative;
    }

    .radio-button-check {
        width: 10px;
        height: 10px;
        border-radius: 50%;
        background-color: #2ca01c;
    }

    .radio-button-active {
        border-color: #2ca01c;
    }

    .radio-button-focus {
        width: 22px;
        height: 22px;
        border-radius: 50%;
        box-sizing: border-box;
        position: absolute;
        top: -3px;
        left: -3px;
    }

    .radio-button:focus .radio-button-focus {
        border: 1px solid #A4DFFF;
    }

</style>