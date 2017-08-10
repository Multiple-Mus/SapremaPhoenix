var inhaleAudio = new Audio('../audio/inhale.mp3');
var exhaleAudio = new Audio('../audio/exhale.mp3');
var holdAudio = new Audio('../audio/hold.mp3');
var audio = new Audio('../audio/ding.mp3');

var breathTime = [];    //seconds of each breath stage
var timeInSecs;         //time left in stage
var position;       //position of breathTime array
var breathText;         //text display on overlay
var ticker;

function SetVolume(val){
    audio.volume = val / 100;
}

function showBreath(){
    document.getElementById("breath-display").style.backgroundColor = "rgba(121,134,203, 0.9)";
    breathText = "inhale";
    document.getElementById("breath-text").innerHTML = breathText;
    document.getElementById("breath-display").style.width = "100%";
}

function hideDisplay(){
    document.getElementById("breath-display").style.width = "0%";
    clearInterval(ticker);
}

function clickThis(){
    breathTime[0] = parseInt(document.getElementById("inhale").value);
    breathTime[1] = parseInt(document.getElementById("inHold").value);
    breathTime[2] = parseInt(document.getElementById("exhale").value);
    breathTime[3] = parseInt(document.getElementById("outHold").value);
    position = 0;
    timeInSecs = breathTime[0];
    startTimer(breathTime[0]);
}

function startTimer(secs){
    audio.play();
    timeInSecs = parseInt(secs);
    ticker = setInterval("tick()", 1000);
}

function tick(){

    var secs;
    if (position == 0){
        secs = timeInSecs;
        breathText = "inhale";

        if (secs > 0){
            timeInSecs--;
        }

        else{
            clearInterval(ticker);
            position++;
            if (breathTime[1] == 0){
                position++;
                startTimer(position[2]);
                timeInSecs = breathTime[2];
            }

            else{
                startTimer(position[1]);
                timeInSecs = breathTime[1];
            }
        }
    }

    else if (position == 1){

        secs = timeInSecs;
        breathText = "hold";

        if (secs > 0){
            timeInSecs--;
        }

        else{
            clearInterval(ticker);
            startTimer(position[2]);
            timeInSecs = breathTime[2];
            position++;
        }
    }

    else if (position == 2){

        secs = timeInSecs;
        breathText = "exhale";

        if (secs > 0){
            timeInSecs--;
        }

        else{
        clearInterval(ticker);
        position++;
            if (breathTime[3] == 0){
                position = 0;
                startTimer(position[0]);
                timeInSecs = breathTime[0];
            }

            else{
                startTimer(position[3]);
                timeInSecs = breathTime[3];
            }
        }
    }

    else if (position == 3){

        secs = timeInSecs;
        breathText = "hold";

        if (secs > 0){
            timeInSecs--;
        }

        else{
            clearInterval(ticker);
            startTimer(position[0]);
            timeInSecs = breathTime[0];
            position = 0;
        }
    }

    document.getElementById("breath-text").innerHTML = breathText;
}

