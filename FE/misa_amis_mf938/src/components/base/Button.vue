<template>
  <button :class="['button', buttonClass]" @click="$emit('btnClick')">
    <p>
      {{ buttonText }}
    </p>
  </button>
</template>

<script>
export default {
  name: 'Button',
  props: {
    buttonClass: String,
    buttonText: String,
  },
};
</script>

<style scoped>
.button {
  outline: 0;
  border: 0;
  cursor: pointer;
  height: 36px;
  width: auto;
  padding-left: 20px;
  padding-right: 20px;
  border-radius: 3px;
  box-sizing: border-box;
  display: flex;
  justify-content: center;
}

.button p {
  line-height: 36px;
  margin: 0;
  padding: 0;
}

.button-primary {
  border: 1px solid transparent;
  color: #fff;
  background-color: #2ca01c;
}

.button-primary:hover {
  background-color: #35bf22 !important;
}

.button-secondary {
    border: 1px solid #8d9096;
    color: #111;
    background-color: transparent;
}

.button-secondary:hover {
  background-color: #d2d3d6 !important;
}
</style>