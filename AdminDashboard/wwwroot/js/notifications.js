function showNotification(type, message) {
    Swal.fire({
        icon: type, // success | error | warning | info
        title: type === 'success' ? 'Success' : 'Error',
        text: message,
        timer: 2500,
        showConfirmButton: false
    });
}