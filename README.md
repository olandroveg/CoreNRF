This "Core NRF" project is part of the "DTT Broadcast Core Network" architecture under development.
Core NRF (Network Repository Function) will be the anchor for the endpoints, addresses and other accesses of the rest of the Network Functions  NF (Microservices) that make up the Broadcast Core Network (BCN).
Each new Network Function (microservice) that registers in the BCN must first register with NRF and provide it with its API endpoints. NRF will store them and provide them to the third party NF that needs to use them.
This process is called Register and Discovery. NRF is part of the "BCN Microservices" project(https://github.com/olandroveg/BCNMicroservices).

Core NRF is configured to use MySql DB.

Developer: Orlando Landrove

ASP.NetCore C# project
