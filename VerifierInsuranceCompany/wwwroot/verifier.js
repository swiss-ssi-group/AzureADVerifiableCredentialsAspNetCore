var signIn = document.getElementById('sign-in');
var signOut = document.getElementById('sign-out');
var display = document.getElementById('display');
var qrcode = new QRCode("qrcode", {width: 300, height: 300 });
var respPresentationReq = null;

signIn.addEventListener('click', () => {
    fetch('/api/verifier/presentation-request')
        .then(function (response) {
            response.text()
                .catch(error => document.getElementById("message").innerHTML = error)
                .then(function (message) {
                    respPresentationReq = JSON.parse(message);
                    if (/Android/i.test(navigator.userAgent)) {
                        console.log(`Android device! Using deep link (${respPresentationReq.url}).`);
                        window.location.href = respPresentationReq.url; setTimeout(function () {
                            window.location.href = "https://play.google.com/store/apps/details?id=com.azure.authenticator";
                        }, 2000);
                    } else if (/iPhone/i.test(navigator.userAgent)) {
                        console.log(`iOS device! Using deep link (${respPresentationReq.url}).`);
                        window.location.replace(respPresentationReq.url);
                    } else {
                        console.log(`Not Android or IOS. Generating QR code encoded with ${message}`);
                        qrcode.makeCode(respPresentationReq.url);
                        document.getElementById('sign-in').style.visibility = "hidden";
                        document.getElementById('qrText').style.display = "block";
                    }
                }).catch(error => { console.log(error.message); })
        }).catch(error => { console.log(error.message); })

    var checkStatus = setInterval(function () {
            if(respPresentationReq){
    fetch('api/verifier/presentation-response?id=' + respPresentationReq.id)
        .then(response => response.text())
        .catch(error => document.getElementById("message").innerHTML = error)
        .then(response => {
            if (response.length > 0) {
                console.log(response)
                respMsg = JSON.parse(response);
                // QR Code scanned
                if (respMsg.status == 'request_retrieved') {
                    document.getElementById('message-wrapper').style.display = "block";
                    document.getElementById('qrText').style.display = "none";
                    document.getElementById('qrcode').style.display = "none";

                    document.getElementById('message').innerHTML = respMsg.message;
                }

                if (respMsg.status == 'presentation_verified') {
                    document.getElementById('message').innerHTML = respMsg.message;
                    document.getElementById('payload').innerHTML = "Payload: " + JSON.stringify(respMsg.payload);
                    document.getElementById('subject').innerHTML = respMsg.name + " is a Verified Credential Driving license";
                    clearInterval(checkStatus);
                }
            }
        })
}
            
    }, 1500); //change this to higher interval if you use ngrok to prevent overloading the free tier service
})
