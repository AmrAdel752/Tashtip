"use strict";
console.clear();

(function () {
  const timer = 3900;

  const root = document.querySelector(":root");
  const bg_width = parseFloat(
    window.getComputedStyle(root).getPropertyValue("--bg_width")
  );
  const bg_height = parseFloat(
    window.getComputedStyle(root).getPropertyValue("--bg_height")
  );

  let items;
  let now = 0;
  let timerId;

  function resize() {
    const ww = window.innerWidth;
    const hh = window.innerHeight;

    // fit size : cover
    let hRatio = ww / bg_width;
    let vRatio = hh / bg_height;
    let ratio = Math.max(hRatio, vRatio);
    root.style.setProperty("--ratio", ratio);
  }
  resize();
  window.onresize = resize;

  //function slider() {
  //  if (now >= items.length) now = 0;
  //  items[now].checked = true;
  //  now++;
  //}
  //window.onload = () => {
  //  items = document.querySelectorAll("input[name='photo']");
  //  now++;
  //  timerId = setInterval(slider, timer);
  //};
})();
