<template>
  <!-- <router-view/> -->
  <router-view :key="$route.fullPath"/>
</template>
<script>
export default {
  name: 'App',
}
</script>
<style>
  body{
    margin: 0;
    padding: 0;
  }
  body::-webkit-scrollbar {
    width: 7px;
  }
  body::-webkit-scrollbar-thumb {
    background-color: #d1c9ff;
    border-radius: 10px;
    border: 1px solid #f4f6f6;
  }
  body::-webkit-scrollbar-track {
    background-color: #f4f6f6;
    border-radius: 10px;
    margin: 10px;
  }
  #app {
    font-family: Avenir, Helvetica, Arial, sans-serif;
    -webkit-font-smoothing: antialiased;
    -moz-osx-font-smoothing: grayscale;
    text-align: center;
    color: #2c3e50;
  }


</style>
