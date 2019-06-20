// Initialize and add the map
var geocoder;
var map;
var mapOptions;

// Function written by me to format address:
function formatAddress() {
    var streetAddress = document.getElementById("streetAddress").value;
    var suburb = document.getElementById("suburb").value;
    var state = document.getElementById("state").value;
    return streetAddress + ", " + suburb + ", " + state;
}


function DeleteMarkers() {
    //Loop through all the markers and remove
    for (var i = 0; i < markers.length; i++) {
        markers[i].setMap(null);
    }
    markers = [];
}


function initMap() {
    var address = formatAddress();

    geocoder = new google.maps.Geocoder();
    geocoder.geocode({ 'address': address }, function (results, status) {
        if (status === 'OK') {
            mapOptions = {
                zoom: 14,
                center: results[0].geometry.location
            };
            map = new google.maps.Map(document.getElementById('map'), mapOptions);
            var marker = new google.maps.Marker({
                map: map,
                position: results[0].geometry.location
            });
        } else {
            var latlng = new google.maps.LatLng(-33.889348, 151.1583713);
            mapOptions = {
                zoom: 8,
                center: latlng
            };
        }
    });
}

function codeAddress() {
    var address = formatAddress();
    DeleteMarkers();
    geocoder.geocode({ 'address': address }, function (results, status) {
        if (status === 'OK') {
            map.setCenter(results[0].geometry.location);
            var marker = new google.maps.Marker({
                map: map,
                position: results[0].geometry.location
            });
        } else {
            alert('Geocode was not successful for the following reason: ' + status);
        }
    });
}