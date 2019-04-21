from __future__ import with_statement
from __future__ import absolute_import
import argparse
import os
from io import open


def get_actual_address():
    response = None
    with open(u'.node.address') as f:
        response = f.readline()
    if response:
        response = response[:response.find(u'\n')]
    return response


def set_new_address(new_address):
    with open(u'.node.address', u'w') as f:
        f.write(new_address)
    print u'Successfully change node address'

def change_directory(node):
    pattern = get_actual_address()
    
    for dir_name, dirs, files in os.walk(u'.'):
        for file_name in files:
            file_path = os.path.join(dir_name, file_name)
            if u'change_host' in file_path:
                continue
            len_file_path = len(file_path)
            if u'.cs' != file_path[len_file_path - 3:len_file_path] and u'.md' != file_path[len_file_path - 3:len_file_path]:
                continue
            file_regex = u''
            with open(file_path) as f:
               for s in f.readlines():
                    if pattern in s and u'restsharp' in dir_name:
                        file_regex += s[:s.find(pattern)]
                        file_regex += node + s[s.find(pattern) + len(pattern):] + u'\n'
                    else:
                        file_regex += s
            if node in file_regex:
                print file_path
                with open(file_path, u"w") as f:
                    f.write(file_regex)


def main():
    parser = argparse.ArgumentParser()
    parser.add_argument(u"-n", u"--node", help=u"Specific Node Address")
    args = parser.parse_args()
    print args

    if args.node:
        print u"Updating server address"
        change_directory(args.node)
        print u'Update server address'
    else:
        print u'set a specific node address'


main()


if __name__ == u'__main__':
    main()
