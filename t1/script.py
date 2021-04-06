#!/usr/bin/env python

# Copyright 2016 Google Inc.
#
# Licensed under the Apache License, Version 2.0 (the "License");
# you may not use this file except in compliance with the License.
# You may obtain a copy of the License at
#
#     http://www.apache.org/licenses/LICENSE-2.0
#
# Unless required by applicable law or agreed to in writing, software
# distributed under the License is distributed on an "AS IS" BASIS,
# WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
# See the License for the specific language governing permissions and
# limitations under the License.

"""Demonstrates how to connect to Cloud Bigtable and run some basic operations.
Prerequisites:
- Create a Cloud Bigtable cluster.
  https://cloud.google.com/bigtable/docs/creating-cluster
- Set your Google Application Default Credentials.
  https://developers.google.com/identity/protocols/application-default-credentials
"""

import argparse
# [START bigtable_hw_imports]
import datetime
import os
from flask import escape

from google.cloud import bigtable
from google.cloud.bigtable import column_family
from google.cloud.bigtable import row_filters
# [END bigtable_hw_imports]

def hello_http(request):
    request_json = request.get_json(silent=True)
    request_args = request.args

    if request_json and 'name' in request_json:
        name = request_json['name']
    elif request_args and 'name' in request_args:
        name = request_args['name']
    else:
        name = 'World'
    return 'Hello {}!'.format(escape(name))

def main(request):
    project_id = "igneous-impulse-280111"
    instance_id = "bigtable"
    table_id = "tablw"
    # [START bigtable_hw_connect]
    # The client must be created with admin=True because it will create a
    # table.
    client = bigtable.Client(project=project_id, admin=True)
    instance = client.instance(instance_id)
    # [END bigtable_hw_connect]

    # [START bigtable_hw_create_table]
    print('Creating the {} table.'.format(table_id))
    table = instance.table(table_id)

    print('Creating column family cf1 with Max Version GC rule...')
    # Create a column family with GC policy : most recent N versions
    # Define the GC policy to retain only the most recent 2 versions
    max_versions_rule = column_family.MaxVersionsGCRule(2)
    column_family_id = 'cf1'
    column_families = {column_family_id: max_versions_rule}
    if not table.exists():
        table.create(column_families=column_families)
    else:
        print("Table {} already exists.".format(table_id))
    # [END bigtable_hw_create_table]

    # [START bigtable_hw_write_rows]
    print('Writing some greetings to the table.')
    greetings = ['Hello World!', 'Hello Cloud Bigtable!', 'Hello Python!']
    rows = []
    column = 'greeting'.encode()
    for i, value in enumerate(greetings):
        # Note: This example uses sequential numeric IDs for simplicity,
        # but this can result in poor performance in a production
        # application.  Since rows are stored in sorted order by key,
        # sequential keys can result in poor distribution of operations
        # across nodes.
        #
        # For more information about how to design a Bigtable schema for
        # the best performance, see the documentation:
        #
        #     https://cloud.google.com/bigtable/docs/schema-design
        row_key = 'greeting{}'.format(i).encode()
        row = table.direct_row(row_key)
        row.set_cell(column_family_id,
                     column,
                     value,
                     timestamp=datetime.datetime.utcnow())
        rows.append(row)
    table.mutate_rows(rows)
    # [END bigtable_hw_write_rows]

    # [START bigtable_hw_create_filter]
    # Create a filter to only retrieve the most recent version of the cell
    # for each column accross entire row.
    row_filter = row_filters.CellsColumnLimitFilter(1)
    # [END bigtable_hw_create_filter]

    # [START bigtable_hw_get_with_filter]
    print('Getting a single greeting by row key.')
    key = 'greeting0'.encode()

    row = table.read_row(key, row_filter)
    cell = row.cells[column_family_id][column][0]
    print(cell.value.decode('utf-8'))
    # [END bigtable_hw_get_with_filter]

    # [START bigtable_hw_delete_table]
    print('Deleting the {} table.'.format(table_id))
    table.delete()
    # [END bigtable_hw_delete_table]

    return 'merge coaie'.format()
    
    
    
def insert(request):

    request_json = request.get_json(silent=True)
    request_args = request.args

    if request_json and 'name' in request_json:
        name = request_json['name']
    elif request_args and 'name' in request_args:
        name = request_args['name']
    else:
        name = 'World'

    value = name

    project_id = "igneous-impulse-280111"
    instance_id = "bigtable"
    table_id = "tablw"

    client = bigtable.Client(project=project_id, admin=True)
    instance = client.instance(instance_id)

    table = instance.table(table_id)

    max_versions_rule = column_family.MaxVersionsGCRule(2)
    column_family_id = 'cf1'
    column_families = {column_family_id: max_versions_rule}
    if not table.exists():
        table.create(column_families=column_families)
    else:
        print("Table {} already exists.".format(table_id))

    print('Writing some greetings to the table.')
    greetings = ['Hello World!', 'Hello Cloud Bigtable!', 'Hello Python!']
    rows = []
    column = 'greeting'.encode()

    numar = 0
    row_filter = row_filters.CellsColumnLimitFilter(1)
    partial_rows = table.read_rows(filter_=row_filter)

    for row in partial_rows:
        numar = numar + 1
        # print(numar)

    row_key = 'greeting{}'.format(numar).encode()
    row = table.direct_row(row_key)
    row.set_cell(column_family_id,
                    column,
                    value,
                    timestamp=datetime.datetime.utcnow())
    rows.append(row)
    table.mutate_rows(rows)


    # print('Deleting the {} table.'.format(table_id))
    # table.delete()

    return str(value)

def get_phone_number(request):

    column_family_id = 'cf1'
    column = 'greeting'.encode()
    request_json = request.get_json(silent=True)
    request_args = request.args

    if request_json and 'name' in request_json:
        name = request_json['name']
    elif request_args and 'name' in request_args:
        name = request_args['name']
    else:
        name = 'ceva'

    project_id = "igneous-impulse-280111"
    instance_id = "bigtable"
    table_id = "tablw"

    client = bigtable.Client(project=project_id, admin=True)
    instance = client.instance(instance_id)
    table = instance.table(table_id)

    partial_rows = table.read_rows()
    ok = 0
    for row in partial_rows:
        cell = row.cells[column_family_id][column][0]
        details = cell.value.decode('utf-8').split('-')
        #print(details)
        if details[0] == name:
            ok = 1
    if ok == 1:
        return 'DA'
    else:
        return 'Nu'


# print(insert())
# print(get_phone_number())