var signIn = document.getElementById('sign-in');
var signOut = document.getElementById('sign-out');
var display = document.getElementById('display');
var qrcode = new QRCode("qrcode", {width: 300, height: 300 });
var respIssuanceReq = null;

    signIn.addEventListener('click', () => {
    fetch('/api/issuer/issuance-request')
        .then(function (response) {
            response.text()
                .catch(error => document.getElementById("message").innerHTML = error)
                .then(function (message) {
                    respIssuanceReq = JSON.parse(message);
                    if (/Android/i.test(navigator.userAgent)) {
                        console.log(`Android device! Using deep link (${respIssuanceReq.url}).`);
                        window.location.href = respIssuanceReq.url; setTimeout(function () {
                            window.location.href = "https://play.google.com/store/apps/details?id=com.azure.authenticator";
                        }, 2000);
                    } else if (/iPhone/i.test(navigator.userAgent)) {
                        console.log(`iOS device! Using deep link (${respIssuanceReq.url}).`);
                        window.location.replace(respIssuanceReq.url);
                    } else {
                        console.log(`Not Android or IOS. Generating QR code encoded with ${message}`);
                        qrcode.makeCode(respIssuanceReq.url);
                        document.getElementById('sign-in').style.display = "none";
                        document.getElementById('qrText').style.display = "block";
                        if (respIssuanceReq.pin) {
                            document.getElementById('pinCodeText').innerHTML = "Pin code: " + respIssuanceReq.pin;
                            document.getElementById('pinCodeText').style.display = "block";
                        }
                    }
                }).catch(error => { console.log(error.message); })
        }).catch(error => { console.log(error.message); })

        var checkStatus = setInterval(function () {
            if(respIssuanceReq){
    fetch('api/issuer/issuance-response?id=' + respIssuanceReq.id)
        .then(response => response.text())
        .catch(error => document.getElementById("message").innerHTML = error)
        .then(response => {
            if (response.length > 0) {
                console.log(response)
                respMsg = JSON.parse(response);
                // QR Code scanned, show pincode if pincode is required
                if (respMsg.status == 'request_retrieved') {
                    document.getElementById('message-wrapper').style.display = "block";
                    document.getElementById('qrText').style.display = "none";
                    document.getElementById('qrcode').style.display = "none";

                    if (respMsg.pin) {
                        document.getElementById('pinCodeText').style.display = "visible";
                    }
                    document.getElementById('message').innerHTML = respMsg.message;
                }
                if (respMsg.status == 'issuance_successful') {
                    document.getElementById('pinCodeText').style.display = "none";
                    document.getElementById('message').innerHTML = respMsg.message;
                    clearInterval(checkStatus);
                }
                if (respMsg.status == 'issuance_error') {
                    document.getElementById('pinCodeText').style.display = "none";
                    document.getElementById('message').innerHTML = "Issuance error occured, did you enter the wrong pincode? Please refresh the page and try again.";
                    document.getElementById('payload').innerHTML = "Payload: " + respMsg.payload;
                    clearInterval(checkStatus);
                }
            }
        })
}
                
        }, 1500); //change this to higher interval if you use ngrok to prevent overloading the free tier service
    })
