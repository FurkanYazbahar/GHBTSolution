// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
const apiBaseUrl = "https://localhost:5001/api";


function addParticipants(eventId, selectedIds, onComplete) {
    const requests = selectedIds.map(pid => {
        return $.post(`${apiBaseUrl}/events/${eventId}/participant/${pid}`);
    });

    Promise.all(requests).then(() => {
        if (typeof onComplete === "function") onComplete();
    });
}

function removeParticipant(eventId, participantId, onComplete) {
    $.ajax({
        url: `${apiBaseUrl}/events/${eventId}/participant/${participantId}`,
        type: "DELETE",
        success: function () {
            if (typeof onComplete === "function") onComplete();
        },
        error: function () {
            alert("Silme başarısız.");
        }
    });
}