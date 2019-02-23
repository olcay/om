var audioControl = '<audio controls autoplay>'
    + '<source src="https://otomatikmuhendis-staging.herokuapp.com/sya/bizolmasak.m4a" type="audio/mp4" />'
    + '</audio>';


var scrAudio = document.getElementById('scrAudio');
scrAudio.insertAdjacentHTML("afterend", audioControl);