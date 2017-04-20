;
window.hardware = {};
window.hardware.devices =
' [ \
  { \
    "id": "1", \
    "image": "images/keylinkbox.jpg", \
    "deviceName": "Keylink", \
    "description": "Keylink features a secure metal lockbox, and automatically detects when a key is removed or returned. It also supports biometric employee identification via the Digital Persona fingerprint reader", \
  "price": "$10000" \
  }, \
  { \
    "id": "2", \
    "image": "images/geo.jpg", \
    "deviceName": "GEO® Cradle for iPod Touch/iPhone", \
    "description": "The perfect companion to our GEO app, the iPodTouch/iPhone cradle boasts a built-in WiFi connection, barcode scanner, and credit card swiper.", \
  "price": "$499.00, iPod Touch™ or iPhone™ sold separately" \
  }, \
  { \
    "id": "3", \
    "image": "images/fingerprintReader.jpg", \
    "deviceName": "DigitalPersona Fingerprint Reader", \
    "description": "Add another layer of security to your system — no setup required. Software supports biometric authentication via the DigitalPersona fingerprint reader.", \
  "price": "$90.00 each" \
  }, \
  { \
    "id": "4", \
    "image": "images/signaturepad.jpg", \
    "deviceName": "Topaz 1x5 Siglite Signature Pad", \
    "description": "A low-cost, pressure-sensitive electronic signature pad that works smoothly with our system. Best for easy front-desk package tracking and recording visitor entry.", \
  "price": "$250.00 each" \
  }, \
  { \
    "id": "5", \
    "image": "images/barcode.jpg", \
    "deviceName": "Motorola Barcode Scanner", \
    "description": "Enhance the package tracking accuracy and data entry speed of your staff. The Motorola Barcode Scanner captures barcoded delivery tracking numbers and stores them in our storage for later tracing or verification.", \
  "price": "$160.00 each" \
  }, \
  { \
    "id": "6", \
    "image": "images/ipad.jpg", \
    "deviceName": "Apple iPad", \
    "description": "Access our system on an iPad and get all your information via an intuitive touchscreen interface — the perfect solution for buildings lacking space or a full front desk.", \
  "price": "$499 each" \
  }, \
  { \
    "id": "7", \
    "image": "images/way2call.jpg", \
    "deviceName": "Way2Call Hi-phone Autodialer", \
    "description": "Save your staff time and eliminate frustration with this dialer modem. It is fully integratable with your front desk analog phone line. With the Way2Call autodialer, your staff can call any resident phone number from your Address Book in one click.", \
  "price": "$319 each" \
  }, \
  { \
    "id": "8", \
    "image": "images/webcam.jpg", \
    "deviceName": "Microsoft Lifecam VX-2000", \
    "description": "Capture photos with this or any TWAIN-compatible webcam for increased security in your building.", \
  "price": "$50 each" \
  }, \
  { \
    "id": "9", \
    "image": "images/labelwriter.jpg", \
    "deviceName": "Dymo LabelPrinter 400 Turbo", \
    "description": "Dymo LabelPrinter 400 integrates with front desk modules for quick, single-click printing of visitor badges and package ID stickers.", \
  "price": "$129 each" \
  } \
  ]';

  (function(){
    document.addEventListener("DOMContentLoaded", main);
    function main(){
        var data = JSON.parse(hardware.devices);
        var content = document.createElement('div');
        content.classList.add('content');
        document.body.appendChild(content);
      for(var i = 0; i < data.length;){
        var column = document.createElement('div');
        column.classList.add('column');
        for(var j = 0; j < 3; ++j, ++i){
          var device = document.createElement('div');
          device.classList.add('device');
          var image = document.createElement('img');
          image.src = data[i].image;
          device.appendChild(image);
          var diveceName = document.createElement('div');
          diveceName.classList.add('diveceName');
          diveceName.innerHTML = data[i].deviceName;
          device.appendChild(diveceName);
          var description = document.createElement('div');
          description.classList.add('description');
          description.innerHTML = data[i].description;
          device.appendChild(description);
          var price = document.createElement('div');
          price.classList.add('price');
          price.innerHTML = data[i].price;
          device.appendChild(price);
          column.appendChild(device);
      }
      content.appendChild(column);
      }
    }
  })();

