// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.

"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/myHub").build();

connection.on("ReceiveMessage", function (user, message) {
    LoadData();
    console.log("Message Received");
});

connection.start().then(function () {
    LoadData();
    console.log("connection Started");
}).catch(function (err) {
    console.log("error");
});

//window.onload = LoadData();

function LoadData() {
    var tbl = $('#messagesTable');    
    $.ajax({
        url: '/Home/GetData',
        contentType: 'application/html ; charset:utf-8',
        type: 'GET',
        dataType: 'html',
        success: function (result) {
            console.log(result);
            var a2 = JSON.parse(result);
            tbl.empty();
            var i = 1;
            $.each(a2, function (key, value) {
                tbl.append('<tr>' + '<td>' + i + '</td>' + '<td>' + value.empName + '</td>' + '<td>' + value.salary + '</td>' + '<td>' + value.deptName + '</td>' + '<td>' + value.designation + '</td>' + '</tr>');
                i = i + 1;
            });
        }
    });
}