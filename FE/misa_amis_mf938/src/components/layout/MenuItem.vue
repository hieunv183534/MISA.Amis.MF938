<template>
    <div class="menu-item">
        <div :class="['item-icon',menuItemClass,{'mr-0': isCollapse}]"></div>
        <p v-if="!isCollapse" class="item-text">{{menuItemText}}</p>
    </div>
</template>

<script>
export default {
    name: 'MenuItem',
    props:{
        menuItemText: String,
        menuItemClass: String,
        isCollapse: {
            type: Boolean,
            default: false
        }
    }
}
</script>

<style scoped>
    .mr-0{
        margin-right: 0px !important;
    }

.menu-item{
    width: 100%;
    height: 42px;
    /* background-color: green; */
    display: flex;
    align-items: center;
    justify-content: flex-start;
    cursor: pointer;
    padding: 0px 14px 0px 14px;
    box-sizing: border-box;
}

.menu-item:hover{
    background-color: #5D5D61;
}

.item-icon{
    margin-right:10px ;
}

.item-text{
    font-size: 13px;
    line-height: 42px;
    color: #fff;
}
</style>