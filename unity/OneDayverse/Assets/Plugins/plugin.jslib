mergeInto(LibraryManager.library, {

  Hello: function () {
    window.alert("Hello, world!");
  },

  GetUsername: function () {
    var returnStr = window.localStorage.getItem("username");
    var bufferSize = lengthBytesUTF8(returnStr) + 1;
    var buffer = _malloc(bufferSize);
    stringToUTF8(returnStr, buffer, bufferSize);
    return buffer;
  },

  setLectureCate: function(data) {
    window.localStorage.setItem("inLectureName", Pointer_stringify(data));
    console.log("setLectureCate is " + data);
  },

  setLectureId: function(data) {
    window.localStorage.setItem("inLectureId", Pointer_stringify(data));
  },

  goLive: function () {
    window.open("/live")
  },

});


