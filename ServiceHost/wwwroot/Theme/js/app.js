const observer = lozad(".img-media" , {
    threshold: 0.9,
    enableAutoReload: true,
    loaded : function(element){
        console.log("تصویر دیگری بارگذاری شد");
    }
});
observer.observe();

console.log("بارگذاری کامل شد !");