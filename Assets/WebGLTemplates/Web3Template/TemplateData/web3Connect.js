if (window.ethereum) {
  web3 = new Web3(window.ethereum);
  // connect popup
  ethereum.enable();

  window.ethereum.on("accountsChanged", function () {
    location.reload();
  });
}
else {
  alert("Non-Ethereum browser detected. Please connect to a wallet");
}