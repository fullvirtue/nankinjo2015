#
# Cookbook Name:: azureprovisioning
# Recipe:: default
#
# Copyright (c) 2015 The Authors, All Rights Reserved.
require 'chef/provisioning/azure_driver'
with_driver 'azure'
with_chef_server "https://chefnankinjo01.japaneast.cloudapp.azure.com/organizations/nankinjo",
:client_name =>
Chef::Config[:node_name],
:signing_key_filename =>
Chef::Config[:client_key]

machine_options = {
    :bootstrap_options => {
      :vm_user => 'localadmin', #required if Windows
      :cloud_service_name => 'nankinjo', #required
      :storage_account_name => 'nankinjo', #required
      :vm_size => 'Standard_D1', #optional
      :location => 'Japan West', #optional
      :tcp_endpoints => '3389:3389', #optional
      :winrm_transport => { #optional
        'https' => { #required (valid values: 'http', 'https')
          :disable_sspi => false, #optional, (default: false)
          :basic_auth_only => false, #optional, (default: false)
          :no_ssl_peer_verification => true #optional, (default: false)
        }
      }
    },
    :password => 'N@nkinjo', #required
    :image_id => 'a699494373c04fc0bc8f2bb1389d6106__Windows-Server-2012-R2-20151022-en.us-127GB.vhd' #required
}

machine 'devnankinjovm03' do
  machine_options machine_options
end
machine 'stgnankinjovm01' do
  machine_options machine_options
end
machine 'nankinjovm01' do
  machine_options machine_options
end
